using System.Diagnostics;

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

        protected override void Log(string msg, ELogLevel severity)
        {
            StackTrace stackTrace = new();
            string? classname = stackTrace.GetFrame(2)?.GetMethod()?.DeclaringType?.FullName;
            string? methodname = stackTrace.GetFrame(2)?.GetMethod()?.Name;

            DateTime date = DateTime.Now;
            using StreamWriter w = File.AppendText(_filepath);
            w.WriteLine(MakeLogEntry(date, severity, classname, methodname, msg));
        }
    }
}
