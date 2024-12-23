using FLG.Cs.Datamodel.Logger;


namespace FLG.Cs.Logger {
    internal class LoggerConsole(string identifier, string networkingId) : FLGLogger(identifier, networkingId) {
        protected override void Log(string logEntry, ELogLevel _)
        {
            Console.WriteLine(logEntry);
        }
    }
}
