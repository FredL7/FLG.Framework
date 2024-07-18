using System.Diagnostics;

using Godot;

using FLG.Cs.Datamodel;
using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;


namespace FLG.Godot.Framework {
    public class GodotLogger : Logger {
        protected override void Log(string msg, ELogLevel severity)
        {
            StackTrace stackTrace = new();
            string? methodname = stackTrace.GetFrame(2)?.GetMethod()?.Name;
            string? classname = stackTrace.GetFrame(2)?.GetMethod()?.DeclaringType?.FullName;

            DateTime date = DateTime.Now;

            var networking = Locator.Instance.Get<INetworkingManager>();
            string entry = MakeLogEntry(networking.LogIdentifier, date, severity, classname, methodname, msg);

            switch (severity)
            {
                case ELogLevel.ERROR:
                    GD.PrintErr(entry);
                    break;
                case ELogLevel.WARN:
                case ELogLevel.INFO:
                case ELogLevel.DEBUG:
                    GD.Print(entry);
                    break;
            }
        }
    }
}
