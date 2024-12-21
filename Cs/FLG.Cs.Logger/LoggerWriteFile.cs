using FLG.Cs.Datamodel.Logger;


namespace FLG.Cs.Logger {
    internal class LoggerWriteFile : FLGLogger {
        private readonly string _logsDir;
        private readonly string _filepath;

        public LoggerWriteFile(string dir)
        {
            _logsDir = dir;

            DateTime date = DateTime.Now;
            string filename = date.ToString(LoggerConstants.FILENAME_DATE_PATTERN);
            System.IO.Directory.CreateDirectory(_logsDir);
            _filepath = Path.Combine(_logsDir, filename + ".log");
        }

        protected override void Log(string logEntry, ELogLevel _)
        {
            using StreamWriter w = File.AppendText(_filepath);
            w.WriteLine(logEntry);
        }
    }
}
