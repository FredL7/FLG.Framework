using Godot;

using FLG.Cs.Datamodel.UI.Layouts;
using FLG.Cs.Datamodel.UI.Widgets;
using FLG.Cs.Datamodel.UI.Widgets.Forms;
using FLG.Cs.Datamodel.UI.Widgets.Text;
using FLG.Cs.Datamodel.UI;

using FLG.Cs.Math;

using FLG.Godot.UI.Widgets;
using FLG.Godot.Helpers;
using FLG.Cs.ServiceLocator;
using FLG.Cs.Datamodel.Logger;


namespace FLG.Godot.UI {
    internal class UIPainter(Node node) {
        private readonly Node _node = node;

        private readonly Dictionary<string, Node> _layouts = [];
        private readonly Dictionary<string, List<Node>> _pages = [];

        public void Draw(UITool tools)
        {
            foreach (var layout in tools.UI.GetLayouts())
                DrawLayout(layout);
        }

        public void Clear()
        {
            SceneHelper.RemoveAllChildrensImmediately(_node);
        }

        public void ChangePage(string pageId, string layoutId, string currentLayout)
        {
            foreach (var page in _pages)
                foreach (var pageItem in page.Value)
                    pageItem.Set("visible", false);

            foreach (var pageItem in _pages[pageId])
                pageItem.Set("visible", true);

            if (currentLayout != layoutId)
            {
                foreach (var layout in _layouts)
                    layout.Value.Set("visible", false);
                _layouts[layoutId].Set("visible", true);
            }
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
                        var targetNode = AddNode(target, FLGVector2.Zero, layoutElementParent.Dimensions, parentNode);

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

        private Control AddNode(string name, ILayoutElement layoutElement, Node parent)
        {
            var position = layoutElement.Position;
            var dimensions = layoutElement.Dimensions;

            return AddNode(name, position, dimensions, parent);
        }

        private Control AddNode(string name, FLGVector2 position, Size dimensions, Node parent)
        {
            Control node = new()
            {
                Name = name,
                Position = new Vector2(position.X, position.Y),
                Size = new Vector2(dimensions.Width, dimensions.Height),
            };
            parent.AddChild(node);
            node.Owner = _node.GetTree().EditedSceneRoot;
            return node;
        }

        private Control AddGridNode(string name, ILayoutElement layoutElement, Node parent)
        {
            var logger = Locator.Instance.Get<ILogManager>();

            var node = AddNode(name, layoutElement, parent);
            if (layoutElement.BackgroundImage != string.Empty)
            {
                logger.Debug($"background image path: {layoutElement.BackgroundImage}");
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
                logger.Debug($"layoutDimensions={layoutElement.Dimensions} OriginalSize={originalSize}, ratio={ratio}, newSize={newSize}");

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
                    var btn = new FLGButton((IButton)layoutElement);
                    node = btn.Draw(parentNode, fromEditor);
                    break;
                case ELayoutElement.LABEL:
                    var label = new FLGLabel((ILabel)layoutElement);
                    node = label.Draw(parentNode, fromEditor);
                    break;
                case ELayoutElement.SPRITE:
                    var sprite = new FLGSprite((ISprite)layoutElement);
                    node = sprite.Draw(parentNode, fromEditor);
                    break;
                case ELayoutElement.TEXT:
                    var text = new FLGText((IText)layoutElement);
                    node = text.Draw(parentNode, fromEditor);
                    break;
                case ELayoutElement.INPUTFIELD:
                    var inputField = new FLGInputField((IInputField)layoutElement);
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
