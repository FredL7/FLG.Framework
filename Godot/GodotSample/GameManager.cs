using Godot;

using FLG.Cs.Datamodel;
using FLG.Cs.Framework;
using FLG.Cs.Model;


namespace FLG.Godot.Sample {
    public partial class GameManager : Node {
        private const string LOGS_RELATIVE_PATH = "/_logs"; // TODO: Move to serialized field to appear in the inspector?

        public override void _Ready()
        {
            GD.Print("FRED");
            InitializeFramework();
        }

        private void InitializeFramework()
        {
            Result result;
            Preferences prefs = new();
            result = FrameworkManager.Instance.InitializeFramework(prefs);
            if (!result) GD.PrintErr(result.GetMessage());

            PreferencesLogs prefsLogs = new()
            {
                loggerType = ELoggerType.WRITE_FILE,
                logsDir = ProjectSettings.GlobalizePath("user://" + LOGS_RELATIVE_PATH),
            };
            result = FrameworkManager.Instance.InitializeLogs(prefsLogs);
            if (!result) GD.PrintErr(result.GetMessage());

            PreferencesNetworking prefsNetworking = new()
            {
                clientType = ENetworkClientType.OFFLINE,
            };
            result = FrameworkManager.Instance.InitializeNetworking(prefsNetworking);
            if (!result) GD.PrintErr(result.GetMessage());

            var uiManager = GetNode("UI/Layouts");
            uiManager.Call("Initialize");
        }

        public override void _Process(double delta)
        {
            FrameworkManager.Instance.Update();
        }
    }
}
