using FLG.Cs.Datamodel;


namespace FLG.Cs.Logger {
    public abstract class LogManager : ILogManager {
        public LogManager(PreferencesLogs prefs) { }

        #region IServiceInstance
        public abstract void OnServiceRegisteredFail();
        public abstract void OnServiceRegistered();
        #endregion IServiceInstance

        protected abstract void Log(string message, ELogLevel severity);

        protected static string MakeLogEntry(string networkId, DateTime date, ELogLevel severity, string? classname, string? methodname, string msg)
        {
            return $"[{date.ToString(LoggerConstants.LOGGING_DATE_PATTERN)}] [{severity.ToPrettyString()}] [${networkId}] [{(classname ?? LoggerConstants.UNKNOWN)}::{(methodname ?? LoggerConstants.UNKNOWN)}()] {msg}";
        }

        public void Error(string msg)
        {
            Log(msg, ELogLevel.ERROR);
            throw new Exception(msg);
        }
        public void Warn(string msg) { Log(msg, ELogLevel.WARN); }
        public void Info(string msg) { Log(msg, ELogLevel.INFO); }
        public void Debug(string msg) { Log(msg, ELogLevel.DEBUG); }

        public void Log(IResult result) { Log(result.GetMessage(), result.GetSeverity()); }
    }
}
