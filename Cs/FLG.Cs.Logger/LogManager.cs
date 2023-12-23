using FLG.Cs.ServiceLocator;
using System.Diagnostics;

namespace FLG.Cs.Logger {
    // TODO: Write as csv instead?
    public class LogManager : ILogManager {
        private const string FILENAME_DATE_PATTERN = @"yyyyddM_HH-mm";
        private const string LOGGING_DATE_PATTERN = @"HH:mm:ss:fff";
        private const string UNKNOWN = "Unknown";

        private string _filepath;

        public LogManager(string logDir) {
            DateTime date = DateTime.Now;
            string filename = date.ToString(FILENAME_DATE_PATTERN);
            System.IO.Directory.CreateDirectory(logDir);
            _filepath = Path.Combine(logDir, filename + ".log");
            Debug("Current log file: " + _filepath);
        }

        #region IServiceInstance
        public bool IsProxy() => false;
        public void OnServiceRegistered() { Debug("Log Manager Registered"); }
        public void OnServiceRegisteredFail() { Error("Log Manager Failed to register"); }
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
