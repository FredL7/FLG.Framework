using FLG.Cs.Datamodel;


namespace FLG.Cs.Logger {
    internal class LoggerNoLogs : Logger {
        protected override void Log(string msg, ELogLevel serverity) { }
    }
}
