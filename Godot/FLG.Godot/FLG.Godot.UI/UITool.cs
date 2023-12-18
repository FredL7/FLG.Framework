using Godot;

using FLG.Cs.Logger;
using FLG.Godot.Helpers;
using FLG.Cs.ServiceLocator;
using FLG.Cs.Factory;
using FLG.Cs.UI;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;
using FLG.Cs.Framework;

namespace FLG.Godot.UI {
    [Tool]
    public partial class UITool : Control {
        private const string LOGS_RELATIVE_PATH = "../../_logs";
        private const string LAYOUTS_RELATIVE_PATH = "UI/Layouts";
        private const string PAGES_RELATIVE_PATH = "UI/Pages";
        private const string SAVES_RELATIVE_PATH = "../../_saves";

        private IUIManager _uiManager;

        public override void _Ready()
        {
            base._Ready();

            if (Engine.IsEditorHint())
            {
                // Must initialize here because it was not called from Godot.Manager (to create)
                Preferences p = new()
                {
                    logsDir = ProjectSettings.GlobalizePath("res://" + LOGS_RELATIVE_PATH),
                    serializerType = Cs.Serialization.ESerializerType.BIN,
                    savesDir = ProjectSettings.GlobalizePath("res://" + SAVES_RELATIVE_PATH),
                    layoutsDir = ProjectSettings.GlobalizePath("res://" + LAYOUTS_RELATIVE_PATH),
                    pagesDir = ProjectSettings.GlobalizePath("res://" + PAGES_RELATIVE_PATH)
                };
                FrameworkManager.Instance.Initialize(p);
            }

            // TODO: Register as UI observer
            // TODO: Register additional pages and layouts (for Widgets / Controllers)
            FrameworkManager.Instance.BootstrapUI();
        }

        private void Clear()
        {
            SceneHelper.RemoveAllChildrensImmediately(this);
        }

        private void DrawUI()
        {
            DrawLayouts();
            DrawPages();
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

        #region Layouts
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
        #endregion Layouts

        #region Pages
        private void DrawPages()
        {
            foreach (var page in _uiManager.GetPages())
                DrawPage(page);
        }

        private void DrawPage(IPage page)
        {
            // Name page content with: $"{content.Getname()} (Page {page.GetName()})"
            // To make sure unique naming
        }
        #endregion Pages
    }
}
