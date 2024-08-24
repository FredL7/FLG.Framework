using Godot;

using FLG.Cs.Datamodel;
using FLG.Cs.Logger;
using FLG.Cs.Math;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI;
using FLG.Godot.Framework;
using FLG.Godot.Helpers;

using sysV2 = System.Numerics.Vector2;
using gdV2 = Godot.Vector2;
using flgLabel = FLG.Godot.UI.Label;
using flgButton = FLG.Godot.UI.Button;

namespace FLG.Godot.UI {
    public class UIManager : IUIObserver {
        private readonly ILogManager _logger;
        private readonly IUIManager _ui;

        private readonly Node _node;

        private readonly Dictionary<string, Node> _layouts = new();
        private readonly Dictionary<string, List<Node>> _pages = new();
        private string _currentLayout = string.Empty;
        private string _currentPage = string.Empty;

        public UIManager(PreferencesFramework prefs, Node node, bool fromEditor)
        {
            var sanitizedPrefs = FrameworkHelper.SanitizePreferences(prefs);

            if (sanitizedPrefs.ui == null)
            {
                throw new Exception("Cannot initialize Godot UIManager without prefs");
            }

            _node = node;

            if (fromEditor)
            {
                if (sanitizedPrefs.logs == null)
                {
                    throw new Exception("Sanitized preferences (logs) should not be null");
                }

                _logger = new LogManager(sanitizedPrefs.logs.Value);
                IUIFactory factory = new UIFactory();

                var ui = new FLG.Cs.UI.UIManager(sanitizedPrefs.ui.Value, _logger, factory);
                ui.SetupUI();
                _ui = ui;
            }
            else
            {
                _logger = Locator.Instance.Get<ILogManager>();
                _logger.AddLogger(new GodotLogger());

                _ui = Locator.Instance.Get<IUIManager>();

                /*
                 * TODO:
                 * Setup UI used to be called FLG.Cs.UI.UIManager::OnServiceRegistered()
                 * But since the Godot logger isn't added at that time (we only add it here)
                 * We miss on all the logs.
                 * However this solution with a cast feels dirty, but I also don't want to expose
                 * SetupUI to IUIManager since it should only be called on initialization
                 */
                FLG.Cs.UI.UIManager uIManager = (FLG.Cs.UI.UIManager)_ui;
                uIManager.SetupUI();

                _ui.AddObserver(this);
            }

            Setup(sanitizedPrefs.ui.Value.homepage);
        }

        private void Setup(string homepage)
        {
            Clear();
            Draw();
            _ui.SetCurrentPage(homepage);
        }

        public void OnCurrentPageChanged(string pageId, string layoutId)
        {
            if (_currentPage != pageId)
            {
                foreach (var page in _pages)
                    foreach (var pageItem in page.Value)
                        pageItem.Set("visible", false);

                foreach (var pageItem in _pages[pageId])
                    pageItem.Set("visible", true);

                if (_currentLayout != layoutId)
                {
                    foreach (var layout in _layouts)
                        layout.Value.Set("visible", false);
                    _layouts[layoutId].Set("visible", true);
                }

                _currentPage = pageId;
                _currentLayout = layoutId;
            }
        }

        private void Clear()
        {
            SceneHelper.RemoveAllChildrensImmediately(_node);
        }

        private void Draw()
        {
            foreach (var layout in _ui.GetLayouts())
                DrawLayout(layout);
        }

        private void DrawLayout(ILayout layout)
        {
            string id = layout.Name;
            var root = layout.Root;
            var layoutNode = AddNode("layout " + id, root, _node);
            _layouts.Add(id, layoutNode);
            DrawLayoutRecursive(layoutNode, root);
        }

        private void DrawLayoutRecursive(Node parentNode, ILayoutElement layoutElementParent)
        {
            var targets = layoutElementParent.GetTargets();
            foreach (var target in targets)
            {
                if (layoutElementParent.HasChildren(target))
                {
                    var parentForAddNode = parentNode;
                    if (target != ILayoutElement.DEFAULT_CHILDREN_TARGET)
                    {
                        var targetNode = AddNode(target, sysV2.Zero, layoutElementParent.Dimensions, parentNode);

                        if (!_pages.ContainsKey(target))
                            _pages.Add(target, new List<Node>());
                        _pages[target].Add(targetNode);

                        targetNode.Set("visible", false);
                        parentForAddNode = targetNode;
                    }

                    foreach (ILayoutElement child in layoutElementParent.GetChildrens(target))
                    {
                        var node = DrawNode(child, parentForAddNode);
                        DrawLayoutRecursive(node, child);
                    }
                }
            }
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
            node.Owner = _node.GetTree().EditedSceneRoot;
            return node;
        }

        private Node AddGridNode(string name, ILayoutElement layoutElement, Node parent)
        {
            var node = AddNode(name, layoutElement, parent);
            if (layoutElement.BackgroundImage != string.Empty)
            {
                _logger.Debug($"background image path: {layoutElement.BackgroundImage}");
                var rect = new TextureRect
                {
                    Name = "backgroundimg",
                    Position = new Vector2(layoutElement.Position.X, layoutElement.Position.Y),
                    Texture = ResourceLoader.Load<Texture2D>("res://" + layoutElement.BackgroundImage),
                    AnchorLeft = 0.5f,
                    AnchorRight = 0.5f,
                    AnchorTop = 0.5f,
                    AnchorBottom = 0.5f
                };

                var originalSize = rect.Texture.GetSize();
                float ratio = 1;
                if (originalSize.X < layoutElement.Dimensions.Width)
                {
                    ratio = layoutElement.Dimensions.Width / originalSize.X;
                }
                else if (originalSize.Y < layoutElement.Dimensions.Height)
                {
                    ratio = layoutElement.Dimensions.Height / originalSize.Y;
                }

                var newSize = new Vector2(originalSize.X * ratio, originalSize.Y * ratio);
                rect.OffsetLeft = -newSize.X / 2f;
                rect.OffsetRight = newSize.X / 2f;
                rect.OffsetTop = -newSize.Y / 2f;
                rect.OffsetBottom = newSize.Y / 2f;
                rect.PivotOffset = new Vector2(newSize.X / 2f, newSize.Y / 2f);
                _logger.Debug($"layoutDimensions={layoutElement.Dimensions} OriginalSize={originalSize}, ratio={ratio}, newSize={newSize}");

                node.AddChild(rect);
                rect.Owner = _node.GetTree().EditedSceneRoot;
            }
            return node;
        }

        private Node DrawNode(ILayoutElement layoutElement, Node parentNode)
        {
            Node node;
            var root = _node.GetTree().EditedSceneRoot;
            var fromEditor = Engine.IsEditorHint();
            bool parentSetter = true;
            switch (layoutElement.Type)
            {
                case ELayoutElement.BUTTON:
                    IWidget<IButton> btn = new flgButton((IButton)layoutElement);
                    node = btn.Draw(parentNode, fromEditor);
                    break;
                case ELayoutElement.LABEL:
                    IWidget<ILabel> label = new flgLabel((ILabel)layoutElement);
                    node = label.Draw(parentNode, fromEditor);
                    break;
                case ELayoutElement.SPRITE:
                    IWidget<ISprite> sprite = new Sprite((ISprite)layoutElement);
                    node = sprite.Draw(parentNode, fromEditor);
                    break;
                case ELayoutElement.TEXT:
                    IWidget<IText> text = new Text((IText)layoutElement);
                    node = text.Draw(parentNode, fromEditor);
                    break;
                case ELayoutElement.INPUTFIELD:
                    IWidget<IInputField> inputField = new InputField((IInputField)layoutElement);
                    node = inputField.Draw(parentNode, fromEditor);
                    break;
                case ELayoutElement.CONTAINER:
                case ELayoutElement.HSTACK:
                case ELayoutElement.VSTACK:
                    node = AddGridNode(layoutElement.Name, layoutElement, parentNode);
                    parentSetter = false;
                    break;
                default:
                    node = AddNode(layoutElement.Name, layoutElement, parentNode);
                    parentSetter = false;
                    break;
            }

            if (parentSetter)
            {
                parentNode.AddChild(node);
                node.Owner = root;
            }

            return node;
        }
    }
}
