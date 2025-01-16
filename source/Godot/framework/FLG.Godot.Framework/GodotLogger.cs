using Godot;

using FLG.Cs.Datamodel.Logger;

using FLG.Cs.Logger;


namespace FLG.Godot.Framework {
    public class GodotLogger(string identifier, string networkingId) : FLGLogger(identifier, networkingId) {
        public override void LogEntry(LogEntry entry)
        {
            switch (entry.severity)
            {
                case ELogLevel.ERROR:
                    GD.PrintErr(entry.ToPrettyString());
                    break;
                case ELogLevel.WARN:
                case ELogLevel.INFO:
                case ELogLevel.DEBUG:
                    GD.Print(entry.ToPrettyString());
                    break;
            }
        }
    }
}
