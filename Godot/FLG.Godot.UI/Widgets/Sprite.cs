using Godot;

using FLG.Cs.Datamodel;

namespace FLG.Godot.UI {
    public class Sprite : IWidget<ISprite> {
        public ISprite Widget { get; private set; }

        public Sprite(ISprite widget)
        {
            Widget = widget;
        }

        public Node Draw(Node parent, bool _)
        {
            var rect = new TextureRect
            {
                Name = Widget.Name,
                Position = new Vector2(Widget.Position.X, Widget.Position.Y),
                Texture = ResourceLoader.Load<Texture2D>("res://" + Widget.Source),
                AnchorLeft = 0f,
                AnchorRight = 0f,
                AnchorTop = 0f,
                AnchorBottom = 0f,
            };

            return rect;
        }
    }
}
