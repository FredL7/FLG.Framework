using System.Text;
using System.Xml.Serialization;


namespace FLG.Cs.Datamodel.Logger {
    [Serializable]
    [XmlRoot("LogEntries")]
    public class LogEntries {
        [XmlElement("LogEntry")]
        public List<LogEntry> Entries { get; set; } = [];
    }

    [Serializable]
    public class LogEntry {
        public required string identifier;
        public required string networkingIdentifier;

        public string classname = string.Empty;
        public string methodname = string.Empty;
        public DateTime date;

        public ELogLevel severity;
        public string message = string.Empty;

        public override string ToString() => ToPrettyString();

        // TODO: Why didn't I use ToString() directly?
        public string ToPrettyString()
        {
            StringBuilder sb = new();
            sb.Append($"[{date.ToString(LoggerConstants.LOGGING_DATE_PATTERN)}]"); sb.Append(' ');
            if (!string.IsNullOrEmpty(identifier))
            {
                sb.Append($"[{identifier}]"); sb.Append(' ');
            }
            if (!string.IsNullOrEmpty(networkingIdentifier))
            {
                sb.Append($"[{networkingIdentifier}]"); sb.Append(' ');
            }
            sb.Append($"[{severity.ToPrettyString()}]"); sb.Append(' ');
            sb.Append($"[{(classname ?? LoggerConstants.UNKNOWN)}::{(methodname ?? LoggerConstants.UNKNOWN)}()]"); sb.Append(' ');
            sb.Append(message);

            return sb.ToString();
        }
    }
}
