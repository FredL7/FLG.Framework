using FLG.Cs.Datamodel.Logger;


namespace FLG.Cs.Logger {
    internal class LoggerNoLogs : FLGLogger {
        protected override void Log(string logEntry, ELogLevel serverity) { }
    }
}
