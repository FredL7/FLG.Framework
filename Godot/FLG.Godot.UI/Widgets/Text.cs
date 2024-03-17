using Godot;

using FLG.Cs.IDatamodel;

namespace FLG.Godot.UI {
    public class Text : IWidget<IText> {
        public IText Widget { get; private set; }

        private RichTextLabel label;

        public Text(IText widget)
        {
            Widget = widget;
        }

        public Node Draw(Node parent, bool fromEditor)
        {
            label = new()
            {
                Name = Widget.Name,
                BbcodeEnabled = true,
                Position = new Vector2(Widget.Position.X, Widget.Position.Y),
                Size = new Vector2(Widget.Dimensions.Width, Widget.Dimensions.Height),
                Text = Widget.Content,
            };

            if (!fromEditor)
            {
                Widget.TextChanged += UpdateText;
            }

            return label;
        }

        public void UpdateText(object sender, EventArgs e)
        {
            label.Text = Widget.Content;
        }
    }
}
