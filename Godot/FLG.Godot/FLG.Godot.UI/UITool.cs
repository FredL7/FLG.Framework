using Godot;

using FLG.Cs.Framework;
using FLG.Cs.IDatamodel;
using FLG.Cs.Math;
using FLG.Cs.ServiceLocator;
using FLG.Godot.Helpers;

using sysV2 = System.Numerics.Vector2;
using gdV2 = Godot.Vector2;
using System.Collections.Generic;

namespace FLG.Godot.UI {
    [Tool]
    public partial class UITool : Control, IUIObserver {
        private const string LOGS_RELATIVE_PATH = "../../_logs"; // TODO: Move to serialized field to appear in the inspector?
        private const string LAYOUTS_RELATIVE_PATH = "../../Cs/ProjectDefs/ProjectDefs.UI/Layouts";
        private const string PAGES_RELATIVE_PATH = "../../Cs/ProjectDefs/ProjectDefs.UI/Pages";

        private IUIManager _uiManager;

        private Dictionary<string, Node> _layouts = new();
        private Dictionary<string, List<Node>> _pages = new();
        private string _currentLayout = string.Empty;
        private string _currentPage = string.Empty;

        public override void _Ready()
        {
            base._Ready();

            if (Engine.IsEditorHint())
            {
                InitializeFramework();

                Clear();
                DrawUI();

                _uiManager.AddObserver(this);
                _uiManager.SetCurrentPage("Sample1"); // TODO: TMP
                _uiManager.SetCurrentPage("Sample2"); // TODO: TMP
            }
        }
        public void OnCurrentPageChanged(string pageId, string layoutId)
        {
            if (_currentPage != pageId)
            {
                foreach(var page in _pages)
                    foreach(var pageItem in page.Value)
                        pageItem.Set("visible", false);

                foreach(var pageItem in _pages[pageId])
                    pageItem.Set("visible", true);

                if (_currentLayout != layoutId)
                {
                    foreach(var layout in _layouts)
                        layout.Value.Set("visible", false);
                    _layouts[layoutId].Set("visible", true);
                }
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
            var position = layoutElement.Position;
            var dimensions = layoutElement.Dimensions;

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
            string id = layout.Name;
            var root = layout.Root;
            var layoutNode = AddNode("layout " + id, root, this);
            _layouts.Add(id, layoutNode);
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
                        var containerNode = AddNode(container, sysV2.Zero, layoutElementParent.Dimensions, parentNode);

                        if (!_pages.ContainsKey(container))
                            _pages.Add(container, new List<Node>());
                        _pages[container].Add(containerNode);

                        containerNode.Set("visible", false);
                        parentForAddNode = containerNode;
                    }

                    foreach (ILayoutElement child in layoutElementParent.GetChildrens(container))
                    {
                        // TODO: Here cast child from ILayoutElement to concrete type (e.g. Label)
                        var node = AddNode(child.Name, child, parentForAddNode);
                        DrawLayoutRecursive(node, child);
                    }

                }
            }
        }
    }
}
