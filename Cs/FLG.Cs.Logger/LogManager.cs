using System.Diagnostics;

using FLG.Cs.Datamodel;


namespace FLG.Cs.Logger {
    public class LogManager : ILogManager {
        private string _logsDir;
        private string _filepath;

        public LogManager(PreferencesLogs prefs)
        {
            _logsDir = prefs.logsDir;

            DateTime date = DateTime.Now;
            string filename = date.ToString(LoggerMetadata.FILENAME_DATE_PATTERN);
            System.IO.Directory.CreateDirectory(_logsDir);
            _filepath = Path.Combine(_logsDir, filename + ".log");
        }

        #region IServiceInstance
        public bool IsProxy() => false;
        public void OnServiceRegisteredFail() { Error("Log Manager Failed to register"); }
        public void OnServiceRegistered()
        {
            Debug("Log Manager Registered");
        }
        #endregion IServiceInstance

        private void Log(string msg, ELogLevel level)
        {
            StackTrace stackTrace = new();
            string? methodname = stackTrace.GetFrame(2)?.GetMethod()?.Name;
            string? classname = stackTrace.GetFrame(2)?.GetMethod()?.DeclaringType?.FullName;

            DateTime date = DateTime.Now;
            using StreamWriter w = File.AppendText(_filepath);
            w.WriteLine($"[{date.ToString(LoggerMetadata.LOGGING_DATE_PATTERN)}] [{level.ToPrettyString()}] [{(classname ?? LoggerMetadata.UNKNOWN)}::{(methodname ?? LoggerMetadata.UNKNOWN)}()] {msg}");
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
