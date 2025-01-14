using FLG.Cs.Datamodel.Logger;


namespace FLG.Cs.Logger {
    internal class LoggerConsole(string identifier, string networkingId) : FLGLogger(identifier, networkingId) {
        public override void LogEntry(LogEntry entry)
        {
            Console.WriteLine(entry.ToPrettyString());
        }
    }
}
