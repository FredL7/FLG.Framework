using FLG.Cs.Datamodel.Validation;


namespace FLG.Cs.Datamodel.Logger {
    public interface ILogger {
        public void SetNetworkingId(string id);

        public void Error(string msg);
        public void Warn(string msg);
        public void Info(string msg);
        public void Debug(string msg);

        public void Log(Result result);
        public void LogEntry(LogEntry logEntry); // Different name to simplify reflection
    }
}
