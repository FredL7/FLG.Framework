namespace FLG.Cs.Logger {
    public class LogManager : ILogManager {
        private const string FILENAME_DATE_PATTERN = @"yyyyddM_HH-mm";
        private const string LOGGING_DATE_PATTERN = @"HH:mm:ss";
        private string _filepath;

        public LogManager(string fileDir)
        {
            DateTime date = DateTime.Now;
            string filename = date.ToString(FILENAME_DATE_PATTERN);
            System.IO.Directory.CreateDirectory(fileDir);
            _filepath = Path.Combine(fileDir, filename + ".log");
        }

        private void Log<T>(T source, string method, string msg, ELogLevel level)
        {
            DateTime date = DateTime.Now;
            using StreamWriter w = File.AppendText(_filepath);
            w.WriteLine($"[{date.ToString(LOGGING_DATE_PATTERN)}] [{level.ToPrettyString()}] [{source}::{method}] {msg}");
        }

        public void Error(Type source, string method, string msg) { Log(source, method, msg, ELogLevel.ERROR); }
        public void Warn(Type source, string method, string msg) { Log(source, method, msg, ELogLevel.WARN); }
        public void Info(Type source, string method, string msg) { Log(source, method, msg, ELogLevel.INFO); }
        public void Debug(Type source, string method, string msg) { Log(source, method, msg, ELogLevel.DEBUG); }
    }
}
