using Godot;

using FLG.Cs.Framework;
using FLG.Cs.Datamodel;


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
                logsDir = ProjectSettings.GlobalizePath("res://" + LOGS_RELATIVE_PATH),
            };
            FrameworkManager.Instance.InitializeLogs(prefsLogs);

            var uiManager = GetNode("UI/Layouts");
            uiManager.Call("Initialize");
        }
    }
}
