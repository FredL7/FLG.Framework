using Godot;

using FLG.Cs.Framework;
using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;
using FLG.Godot.Helpers;

namespace FLG.Godot.UI {
    [Tool]
    public partial class UITool : Control {
        private const string LOGS_RELATIVE_PATH = "../../_logs"; // TODO: Move to serialized field to appear in the inspector?
        private const string LAYOUTS_RELATIVE_PATH = "../../Cs/ProjectDefs/ProjectDefs.UI/Layouts";
        private const string PAGES_RELATIVE_PATH = "../../Cs/ProjectDefs/ProjectDefs.UI/Pages";

        private IUIManager _uiManager;

        public override void _Ready()
        {
            base._Ready();

            if (Engine.IsEditorHint())
            {
                InitializeFramework();

                Clear();
                DrawUI();
            }
        }

        private void InitializeFramework()
        {
            Preferences prefs = new();
            FrameworkManager.Instance.Initialize(prefs);

            PreferencesLogs prefsLogs = new()
            {
                logsDir = LOGS_RELATIVE_PATH
            };
            FrameworkManager.Instance.InitializeLogs(prefsLogs);

            PreferencesUI prefsUI = new()
            {
                layoutsDir = ProjectSettings.GlobalizePath("res://" + LAYOUTS_RELATIVE_PATH),
                pagesDir = ProjectSettings.GlobalizePath("res://" + PAGES_RELATIVE_PATH)
            };
            FrameworkManager.Instance.InitializeUI(prefsUI);

            _uiManager = Locator.Instance.Get<IUIManager>();
        }

        private void Clear()
        {
            SceneHelper.RemoveAllChildrensImmediately(this);
        }

        private void DrawUI()
        {
            DrawLayouts();
        }

        private Node AddNode(string name, ILayoutElement layoutElement, Node parent)
        {
            var position = layoutElement.GetPosition();
            var dimensions = layoutElement.GetDimensions();

            Control node = new()
            {
                Name = name,
                Position = new(position.X, position.Y),
                Size = new(dimensions.Width, dimensions.Height),
            };
            parent.AddChild(node);
            node.Owner = GetTree().EditedSceneRoot;
            return node;
        }

        private void DrawLayouts()
        {
            foreach (var layout in _uiManager.GetLayouts())
                DrawLayout(layout);
        }

        private void DrawLayout(ILayout layout)
        {
            var root = layout.GetRoot();
            var layoutNode = AddNode("layout " + layout.GetName(), root, this);
            DrawLayoutRecursive(layoutNode, root);
        }

        private void DrawLayoutRecursive(Node parentNode, ILayoutElement layoutElementParent)
        {
            foreach (ILayoutElement child in layoutElementParent.GetChildrens())
            {
                var node = AddNode(child.GetName(), child, parentNode);
                DrawLayoutRecursive(node, child);
            }
        }
    }
}
