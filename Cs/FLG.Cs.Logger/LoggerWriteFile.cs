using System.Xml.Serialization;

using FLG.Cs.Datamodel.Logger;


namespace FLG.Cs.Logger {
    [Serializable]
    [XmlRoot("LogEntries")]
    public class LogEntries {
        [XmlElement("LogEntry")]
        public List<LogEntry> Entries { get; set; } = [];
    }

    internal class LoggerWriteFile : FLGLogger {
        private readonly string _logsDir;
        private readonly string _filepath;

        private readonly XmlSerializer _serializer;

        private readonly LogEntries _entries;

        public LoggerWriteFile(string dir, string identifier, string networkingId)
            : base(identifier, networkingId)
        {
            _logsDir = dir;

            _serializer = new(typeof(LogEntries));

            DateTime date = DateTime.Now;
            string filename = date.ToString(LoggerConstants.FILENAME_DATE_PATTERN);
            System.IO.Directory.CreateDirectory(_logsDir);
            _filepath = Path.Combine(_logsDir, filename + ".log");

            if (!File.Exists(_filepath))
            {
                _entries = new LogEntries();
            }
            else
            {
                using StreamReader reader = new(_filepath);
                _entries = _serializer.Deserialize(reader) as LogEntries ?? new();
            }
        }

        public override void LogEntry(LogEntry entry)
        {
            // TODO: might cause performance issue if it writes the whole xml each time
            _entries.Entries.Add(entry);
            using StreamWriter w = new(_filepath, false);
            _serializer.Serialize(w, _entries);
        }
    }
}
