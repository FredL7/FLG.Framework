using FLG.Cs.IDatamodel;
using Godot;

namespace FLG.Godot.UI.Widgets {
    internal class Sprite : IWidget<ISprite> {
        public ISprite Widget { get; private set; }

        public Sprite(ISprite widget)
        {
            Widget = widget;
        }

        public Node Draw(Node parent, bool _)
        {
            var image = Image.LoadFromFile("res://" + Widget.Source);
            var texture = ImageTexture.CreateFromImage(image);
            Sprite2D sprite = new()
            {
                Name = Widget.Name + " sprite",
                Position = new Vector2(Widget.Position.X, Widget.Position.Y),
                Texture = texture,
            };
            return sprite;
        }
    }
}
