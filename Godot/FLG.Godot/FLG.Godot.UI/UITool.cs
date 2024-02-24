using Godot;

using FLG.Cs.Framework;
using FLG.Cs.IDatamodel;
using FLG.Cs.Math;
using FLG.Cs.ServiceLocator;
using FLG.Godot.Helpers;

using sysV2 = System.Numerics.Vector2;
using gdV2 = Godot.Vector2;

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

                // TODO: Register layout observer
                // TODO: Set current page
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

            return AddNode(name, position, dimensions, parent);
        }

        private Node AddNode(string name, sysV2 position, Size dimensions, Node parent)
        {
            Control node = new()
            {
                Name = name,
                Position = new gdV2(position.X, position.Y),
                Size = new gdV2(dimensions.Width, dimensions.Height),
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
            var containers = layoutElementParent.GetContainers();
            foreach (var container in containers)
            {
                if(layoutElementParent.HasChildren(container))
                {
                    var parentForAddNode = parentNode;
                    if (container != ILayoutElement.DEFAULT_CHILDREN_CONTAINER)
                    {
                        var containerNode = AddNode(container, sysV2.Zero, layoutElementParent.GetDimensions(), parentNode);
                        containerNode.Set("visible", false);
                        parentForAddNode = containerNode;
                    }

                    foreach (ILayoutElement child in layoutElementParent.GetChildrens(container))
                    {
                        var node = AddNode(child.GetName(), child, parentForAddNode);
                        DrawLayoutRecursive(node, child);
                    }

                }
            }
        }
    }
}
