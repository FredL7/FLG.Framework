using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Validation;
using System.Diagnostics;


namespace FLG.Cs.Logger {
    public abstract class FLGLogger : ILogger {
        protected abstract void Log(string logEntry, ELogLevel severity);

        private void LogWrapper(string message, ELogLevel severity)
        {
            StackTrace stackTrace = new();
            string? classname = stackTrace.GetFrame(3)?.GetMethod()?.DeclaringType?.FullName;
            string? methodname = stackTrace.GetFrame(3)?.GetMethod()?.Name;
            DateTime date = DateTime.Now;

            var logEntry = $"[{date.ToString(LoggerConstants.LOGGING_DATE_PATTERN)}] [{severity.ToPrettyString()}] [{(classname ?? LoggerConstants.UNKNOWN)}::{(methodname ?? LoggerConstants.UNKNOWN)}()] {message}";
            Log(logEntry, severity);
        }

        public void Error(string msg)
        {
            LogWrapper(msg, ELogLevel.ERROR);
            throw new Exception(msg);
        }
        public void Warn(string msg) { LogWrapper(msg, ELogLevel.WARN); }
        public void Info(string msg) { LogWrapper(msg, ELogLevel.INFO); }
        public void Debug(string msg) { LogWrapper(msg, ELogLevel.DEBUG); }

        public void Log(Result result) {
            LogWrapper(result.GetMessage(), result.GetSeverity());
        }
    }
}
