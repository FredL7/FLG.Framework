using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Validation;


namespace FLG.Cs.Logger {
    public abstract class FLGLogger : ILogger {
        protected abstract void Log(string message, ELogLevel severity);

        protected static string MakeLogEntry(DateTime date, ELogLevel severity, string? classname, string? methodname, string msg)
        {
            return $"[{date.ToString(LoggerConstants.LOGGING_DATE_PATTERN)}] [{severity.ToPrettyString()}] [{(classname ?? LoggerConstants.UNKNOWN)}::{(methodname ?? LoggerConstants.UNKNOWN)}()] {msg}";
        }

        public void Error(string msg)
        {
            Log(msg, ELogLevel.ERROR);
            throw new Exception(msg);
        }
        public void Warn(string msg) { Log(msg, ELogLevel.WARN); }
        public void Info(string msg) { Log(msg, ELogLevel.INFO); }
        public void Debug(string msg) { Log(msg, ELogLevel.DEBUG); }

        public void Log(Result result) { Log(result.GetMessage(), result.GetSeverity()); }
    }
}
