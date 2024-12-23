using System.Diagnostics;

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

        public abstract void LogEntry(LogEntry logEntry);

        private void LogWrapper(string message, ELogLevel severity)
        {
            StackTrace stackTrace = new();
            LogEntry entry = new()
            {
                identifier = _identifier,
                networkingIdentifier = _networkingIdentifier,
                classname = stackTrace.GetFrame(3)?.GetMethod()?.DeclaringType?.FullName,
                methodname = stackTrace.GetFrame(3)?.GetMethod()?.Name,
                date = DateTime.Now,
                message = message,
                severity = severity
            };
            LogEntry(entry);
        }

        public void Error(string msg)
        {
            LogWrapper(msg, ELogLevel.ERROR);
            throw new Exception(msg);
        }
        public void Warn(string msg) { LogWrapper(msg, ELogLevel.WARN); }
        public void Info(string msg) { LogWrapper(msg, ELogLevel.INFO); }
        public void Debug(string msg) { LogWrapper(msg, ELogLevel.DEBUG); }

        public void Log(Result result)
        {
            LogWrapper(result.GetMessage(), result.GetSeverity());
        }
    }
}
