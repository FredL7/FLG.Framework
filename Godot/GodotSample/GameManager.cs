using Godot;

using FLG.Cs.Datamodel;
using FLG.Godot.UI;
using FLG.Godot.Helpers;

namespace FLG.Godot.Sample {
    [Tool]
    public partial class GameManager : Node {
        private UIManager _ui;

        public override void _Ready()
        {
            InitializeFramework();
        }

        private void InitializeFramework()
        {
            PreferencesFramework prefs = new()
            {
                logs = new()
                {
                    types = new[] { ELoggerType.WRITE_FILE },
                    dir = "/_logs", // ProjectSettings.GlobalizePath("user://" + LOGS_RELATIVE_PATH),
                }, // or null
                ui = new()
                {
                    dirs = new[] { "../commons/ProjectDefs.UI/ui/general/", "../commons/ProjectDefs.UI/ui/client/" },
                    windowSize = new(1920, 1080),
                    homepage = "Sample1"
                }, // or null
                networking = new()
                {
                    clientType = ENetworkClientType.OFFLINE,
                }, // or null
                serialization = new()
                {
                    type = ESerializerType.BIN,
                    dir = "/_saves",
                }, // or null
            };

            if (!Engine.IsEditorHint())
            {
                FrameworkHelper.InitializeFramework(prefs);
            }

            var uiNode = GetNode("UI/Layouts");
            _ui = new(prefs, uiNode, Engine.IsEditorHint());
        }

        public override void _Process(double delta)
        {
            // FrameworkManager.Instance.Update();
        }
    }
}
