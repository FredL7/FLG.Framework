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

            var originalSize = rect.Texture.GetSize();
            float scale = 1;

            if (Widget.Size.Width == 0 && Widget.Size.Height == 0)
            {
                // Keep original image size
            }
            else if (Widget.Size.Width == 0)
            {
                scale = Widget.Size.Height / originalSize.Y;
            }
            else if (Widget.Size.Height == 0)
            {
                scale = Widget.Size.Width / originalSize.X;
            }
            else
            {
                // Both dimensions should compute the same ratio
                scale = Widget.Size.Width / originalSize.X;
            }

            rect.Scale = new(scale, scale);

            return rect;
        }
    }
}
