using Godot;

using FLG.Cs.Datamodel;
using FLG.Cs.Framework;


namespace FLG.Godot.Sample {
    public partial class GameManager : Node {
        private const string LOGS_RELATIVE_PATH = "/_logs"; // TODO: Move to serialized field to appear in the inspector?

        public override void _Ready()
        {
            InitializeFramework();
        }

        private void InitializeFramework()
        {
            Preferences prefs = new();
            FrameworkManager.Instance.InitializeFramework(prefs);

            PreferencesLogs prefsLogs = new()
            {
                logsDir = ProjectSettings.GlobalizePath("user://" + LOGS_RELATIVE_PATH),
            };
            FrameworkManager.Instance.InitializeLogs(prefsLogs);

            PreferencesNetworking prefsNetworking = new();
            FrameworkManager.Instance.InitializeNetworking(prefsNetworking);

            var uiManager = GetNode("UI/Layouts");
            uiManager.Call("Initialize");
        }

        public override void _Process(double delta)
        {
            FrameworkManager.Instance.Update();
        }
    }
}
