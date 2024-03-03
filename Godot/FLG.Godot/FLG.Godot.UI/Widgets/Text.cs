using Godot;

using FLG.Cs.IDatamodel;

namespace FLG.Godot.UI.Widgets {
    internal class Text : IWidget<IText> {
        public IText Widget { get; private set; }

        public Text(IText Widget)
        {
            this.Widget = Widget;
        }

        public Node Draw(Node parent, Node root, bool _)
        {
            RichTextLabel label = new()
            {
                Name = Widget.Name + " text",
                BbcodeEnabled = true,
                Position = new Vector2(Widget.Position.X, Widget.Position.Y),
                Size = new Vector2(Widget.Dimensions.Width, Widget.Dimensions.Height),
                Text = Widget.Value,
            };
            parent.AddChild(label);
            label.Owner = root;
            return label;
        }
    }
}
