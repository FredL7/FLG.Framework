﻿using System.Diagnostics;

using FLG.Cs.Datamodel;
using FLG.Cs.Model;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Logger {
    public class LogManagerNetworking : LogManager {
        public LogManagerNetworking(PreferencesLogs prefs) : base(prefs) { }

        #region IServiceInstance
        public override void OnServiceRegisteredFail() { }
        public override void OnServiceRegistered()
        {
            Debug("Log Manager (Networking) Registered");
        }
        #endregion IServiceInstance

        protected override void Log(string msg, ELogLevel severity)
        {
            var network = Locator.Instance.Get<INetworkingManagerClient>();

            StackTrace stackTrace = new();
            string? methodname = stackTrace.GetFrame(2)?.GetMethod()?.Name;
            string? classname = stackTrace.GetFrame(2)?.GetMethod()?.DeclaringType?.FullName;
            DateTime date = DateTime.Now;
            string logMessage = MakeLogEntry(network.LogIdentifier, date, severity, methodname, classname, msg);

            var command = new Command<ILogManager>(severity.ToLogMethod());
            command.AddParam(logMessage);

            network.SendCommand(command);
        }
    }
}
