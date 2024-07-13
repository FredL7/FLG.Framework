using System.Diagnostics;

using FLG.Cs.Datamodel;


namespace FLG.Cs.Logger {
    public class LogManagerWriteFile : LogManager {
        private readonly string _logsDir;
        private readonly string _filepath;

        public LogManagerWriteFile(PreferencesLogs prefs) : base(prefs)
        {
            _logsDir = prefs.logsDir;

            DateTime date = DateTime.Now;
            string filename = date.ToString(LoggerConstants.FILENAME_DATE_PATTERN);
            System.IO.Directory.CreateDirectory(_logsDir);
            _filepath = Path.Combine(_logsDir, filename + ".log");
        }

        #region IServiceInstance
        public override void OnServiceRegisteredFail() { }
        public override void OnServiceRegistered()
        {
            Debug("Log Manager (Write File) Registered");
        }
        #endregion IServiceInstance

        protected override void Log(string msg, ELogLevel serverity)
        {
            StackTrace stackTrace = new();
            string? methodname = stackTrace.GetFrame(2)?.GetMethod()?.Name;
            string? classname = stackTrace.GetFrame(2)?.GetMethod()?.DeclaringType?.FullName;

            DateTime date = DateTime.Now;
            using StreamWriter w = File.AppendText(_filepath);
            w.WriteLine($"[{date.ToString(LoggerConstants.LOGGING_DATE_PATTERN)}] [{serverity.ToPrettyString()}] [{(classname ?? LoggerConstants.UNKNOWN)}::{(methodname ?? LoggerConstants.UNKNOWN)}()] {msg}");
        }
    }
}
