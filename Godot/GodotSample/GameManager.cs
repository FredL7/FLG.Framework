using Godot;

using FLG.Cs.Datamodel;
using FLG.Cs.Framework;


namespace FLG.Godot.Sample {
    public partial class GameManager : Node {
        private const string LOGS_RELATIVE_PATH = "/_logs"; // TODO: Move to serialized field to appear in the inspector?

        private FrameworkManager _frameworkManager;

        public override void _Ready()
        {
            _frameworkManager = FrameworkManager.Instance;
            InitializeFramework();
        }

        private void InitializeFramework()
        {
            Preferences prefs = new();
            _frameworkManager.InitializeFramework(prefs);

            PreferencesLogs prefsLogs = new()
            {
                logsDir = ProjectSettings.GlobalizePath("res://" + LOGS_RELATIVE_PATH),
            };
            _frameworkManager.InitializeLogs(prefsLogs);

            PreferencesNetworking prefsNetworking = new();
            _frameworkManager.InitializeNetworking(prefsNetworking);

            var uiManager = GetNode("UI/Layouts");
            uiManager.Call("Initialize");
        }

        public override void _Process(double delta)
        {
            _frameworkManager.Update();
        }
    }
}
