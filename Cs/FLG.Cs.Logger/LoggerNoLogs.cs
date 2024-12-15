using FLG.Cs.Datamodel.Logger;


namespace FLG.Cs.Logger {
    internal class LoggerNoLogs : FLGLogger {
        protected override void Log(string msg, ELogLevel serverity) { }
    }
}
