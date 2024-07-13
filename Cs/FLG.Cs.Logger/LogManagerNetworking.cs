using System.Diagnostics;

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

        protected override void Log(string msg, ELogLevel serverity)
        {
            var network = Locator.Instance.Get<INetworkingManagerClient>();
            // TODO: Add client ID to command

            StackTrace stackTrace = new();
            string? methodname = stackTrace.GetFrame(2)?.GetMethod()?.Name;
            string? classname = stackTrace.GetFrame(2)?.GetMethod()?.DeclaringType?.FullName;
            DateTime date = DateTime.Now;
            string logMessage = $"[{date.ToString(LoggerConstants.LOGGING_DATE_PATTERN)}] [{serverity.ToPrettyString()}] [{(classname ?? LoggerConstants.UNKNOWN)}::{(methodname ?? LoggerConstants.UNKNOWN)}()] {msg}";

            var command = new Command<ILogManager>(serverity.ToLogMethod());
            command.AddParam(logMessage);

            network.SendCommand(command);
        }
    }
}
