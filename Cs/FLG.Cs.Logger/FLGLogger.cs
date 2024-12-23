using System.Diagnostics;
using System.Text;

using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Validation;


namespace FLG.Cs.Logger {
    public abstract class FLGLogger(string identifier, string networkingId) : ILogger {
        private readonly string _identifier = identifier;
        private string _networkingIdentifier = networkingId;

        public void SetNetworkingId(string id)
        {
            _networkingIdentifier = id;
        }

        protected abstract void Log(string logEntry, ELogLevel severity);

        private void LogWrapper(string message, ELogLevel severity, bool external)
        {
            if (external)
            {
                // When a log message comes from a connected client, it will already be correctly formatted
                Log(message, severity);
            }
            else
            {
                StackTrace stackTrace = new();
                string? classname = stackTrace.GetFrame(3)?.GetMethod()?.DeclaringType?.FullName;
                string? methodname = stackTrace.GetFrame(3)?.GetMethod()?.Name;
                DateTime date = DateTime.Now;

                StringBuilder sb = new();
                sb.Append($"[{date.ToString(LoggerConstants.LOGGING_DATE_PATTERN)}]"); sb.Append(' ');
                if (!string.IsNullOrEmpty(_identifier))
                {
                    sb.Append($"[{_identifier}]"); sb.Append(' ');
                }
                if (!string.IsNullOrEmpty(_networkingIdentifier))
                {
                    sb.Append($"[{_networkingIdentifier}]"); sb.Append(' ');
                }
                sb.Append($"[{severity.ToPrettyString()}]"); sb.Append(' ');
                sb.Append($"[{(classname ?? LoggerConstants.UNKNOWN)}::{(methodname ?? LoggerConstants.UNKNOWN)}()]"); sb.Append(' ');
                sb.Append(message);
                Log(sb.ToString(), severity);
            }
        }

        public void Error(string msg, bool external = false)
        {
            LogWrapper(msg, ELogLevel.ERROR, external);
            throw new Exception(msg);
        }
        public void Warn(string msg, bool external = false) { LogWrapper(msg, ELogLevel.WARN, external); }
        public void Info(string msg, bool external = false) { LogWrapper(msg, ELogLevel.INFO, external); }
        public void Debug(string msg, bool external = false) { LogWrapper(msg, ELogLevel.DEBUG, external); }

        public void Log(Result result)
        {
            LogWrapper(result.GetMessage(), result.GetSeverity(), false);
        }
    }
}
