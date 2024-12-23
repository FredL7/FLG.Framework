using FLG.Cs.Datamodel.Logger;


namespace FLG.Cs.Logger {
    internal class LoggerNoLogs(string identifier, string networkingId) : FLGLogger(identifier, networkingId) {
        public override void LogEntry(LogEntry _) { }
    }
}
