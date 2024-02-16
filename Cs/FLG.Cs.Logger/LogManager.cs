using System.Diagnostics;

using FLG.Cs.IDatamodel;


namespace FLG.Cs.Logger {
    public class LogManager : ILogManager {
        private const string FILENAME_DATE_PATTERN = @"yyyyddM_HH-mm";
        private const string LOGGING_DATE_PATTERN = @"HH:mm:ss:fff";
        private const string UNKNOWN = "Unknown";

        private string _logsDir;
        private string _filepath;

        public LogManager(string logsDir) {
            _logsDir = logsDir;

            DateTime date = DateTime.Now;
            string filename = date.ToString(FILENAME_DATE_PATTERN);
            System.IO.Directory.CreateDirectory(_logsDir);
            _filepath = Path.Combine(_logsDir, filename + ".log");
        }

        #region IServiceInstance
        public bool IsProxy() => false;
        public void OnServiceRegisteredFail() { Error("Log Manager Failed to register"); }
        public void OnServiceRegistered() {
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
            w.WriteLine($"[{date.ToString(LOGGING_DATE_PATTERN)}] [{level.ToPrettyString()}] [{(classname ?? UNKNOWN)}::{(methodname ?? UNKNOWN)}()] {msg}");
        }

        public void Error(string msg)
        {
            Log(msg, ELogLevel.ERROR);
            throw new Exception(msg);
        }
        public void Warn(string msg) { Log(msg, ELogLevel.WARN); }
        public void Info(string msg) { Log(msg, ELogLevel.INFO); }
        public void Debug(string msg) { Log(msg, ELogLevel.DEBUG); }
    }
}
