using FLG.Cs.Logger;
using FLG.Cs.Datamodel.Logger;


namespace FLG.Unity.Logger {
    public class UnityLogger(string identifier, string networkingId) : FLGLogger(identifier, networkingId) {
        public override void LogEntry(LogEntry logEntry)
        {
            switch (logEntry.severity)
            {
                case ELogLevel.ERROR: UnityEngine.Debug.LogError(logEntry.ToPrettyString()); break;
                case ELogLevel.WARN: UnityEngine.Debug.LogWarning(logEntry.ToPrettyString()); break;
                default: UnityEngine.Debug.Log(logEntry.ToPrettyString()); break;
            }
        }
    }
}
