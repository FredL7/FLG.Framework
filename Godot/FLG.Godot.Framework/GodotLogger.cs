﻿using System.Diagnostics;

using Godot;

using FLG.Cs.Datamodel;

namespace FLG.Godot.Framework {
    public class GodotLogger : ILogManager {
        #region IServiceInstance
        public void OnServiceRegisteredFail() { throw new NotImplementedException("Should not register Godot Logger"); }
        public void OnServiceRegistered() { throw new NotImplementedException("Should not register Godot Logger"); }
        #endregion IServiceInstance

        private void Log(string msg, ELogLevel level)
        {
            StackTrace stackTrace = new();
            string? methodname = stackTrace.GetFrame(2)?.GetMethod()?.Name;
            string? classname = stackTrace.GetFrame(2)?.GetMethod()?.DeclaringType?.FullName;

            DateTime date = DateTime.Now;
            GD.Print($"[{date.ToString(LoggerMetadata.LOGGING_DATE_PATTERN)}] [{level.ToPrettyString()}] [{(classname ?? LoggerMetadata.UNKNOWN)}::{(methodname ?? LoggerMetadata.UNKNOWN)}()] {msg}");
            // TODO: use GD.PushError and GD.PushWarning
        }

        public void Error(string msg)
        {
            Log(msg, ELogLevel.ERROR);
            throw new Exception(msg);
        }
        public void Warn(string msg) { Log(msg, ELogLevel.WARN); }
        public void Info(string msg) { Log(msg, ELogLevel.INFO); }
        public void Debug(string msg) { Log(msg, ELogLevel.DEBUG); }

        public void Log(IResult result)
        {
            Log(result.GetMessage(), result.GetSeverity());
        }
    }
}
