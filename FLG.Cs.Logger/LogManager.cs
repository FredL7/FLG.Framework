using FLG.Cs.Decorators;
using System.Diagnostics;

namespace FLG.Cs.Logger {
    // TODO: Write as csv instead?
    public class LogManager :  SingletonBase<LogManager> {
        private const string FILENAME_DATE_PATTERN = @"yyyyddM_HH-mm";
        private const string LOGGING_DATE_PATTERN = @"HH:mm:ss:fff";
        private const string UNKNOWN = "Unknown";

        private string _filepath = String.Empty; // TODO: Default location?

        private LogManager() { }

        public void SetLogLocation(string logDir)
        {
            DateTime date = DateTime.Now;
            string filename = date.ToString(FILENAME_DATE_PATTERN);
            System.IO.Directory.CreateDirectory(logDir);
            _filepath = Path.Combine(logDir, filename + ".log");
            Debug("LogManager start");
        }

        private void Log(string msg, ELogLevel level)
        {
            if (string.IsNullOrEmpty(_filepath))
                throw new Exception("No directory specified for logs, use SetLogLocation()");

            StackTrace stackTrace = new();
            string? methodname = stackTrace.GetFrame(2)?.GetMethod()?.Name;
            string? classname = stackTrace.GetFrame(2)?.GetMethod()?.DeclaringType?.FullName;

            DateTime date = DateTime.Now;
            using StreamWriter w = File.AppendText(_filepath);
            w.WriteLine($"[{date.ToString(LOGGING_DATE_PATTERN)}] [{level.ToPrettyString()}] [{(classname ?? UNKNOWN)}::{(methodname ?? UNKNOWN)}()] {msg}");
        }

        public void Error(string msg) {
            Log(msg, ELogLevel.ERROR);
            throw new Exception(msg);
        }
        public void Warn(string msg) { Log(msg, ELogLevel.WARN); }
        public void Info(string msg) { Log(msg, ELogLevel.INFO); }
        public void Debug(string msg) { Log(msg, ELogLevel.DEBUG); }
    }
}
