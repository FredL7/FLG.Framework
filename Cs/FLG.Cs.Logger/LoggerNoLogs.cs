using FLG.Cs.Datamodel.Logger;


namespace FLG.Cs.Logger {
    internal class LoggerNoLogs(string identifier, string networkingId) : FLGLogger(identifier, networkingId) {
        protected override void Log(string logEntry, ELogLevel serverity) { }
    }
}
