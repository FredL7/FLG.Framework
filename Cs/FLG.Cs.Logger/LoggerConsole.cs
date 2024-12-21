using FLG.Cs.Datamodel.Logger;


namespace FLG.Cs.Logger {
    internal class LoggerConsole : FLGLogger {
        protected override void Log(string logEntry, ELogLevel _)
        {
            Console.WriteLine(logEntry);
        }
    }
}
