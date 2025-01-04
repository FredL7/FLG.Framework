using FLG.Cs.Datamodel;
using Godot;

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
                Text = TextAlign(Widget.Content),
                // Vertical alignment will require a container, resizing the rich text to nb lines * line height and adding an offset
            };

            if (!fromEditor)
            {
                Widget.TextChanged += UpdateText;
            }

            return label;
        }

        public void UpdateText(object sender, EventArgs e)
        {
            label.Text = TextAlign(Widget.Content);
        }

        public string TextAlign(string content)
        {
            return Widget.AlignHorizontal switch
            {
                ETextAlignHorizontal.CENTER => "[center]" + content + "[/center]",
                ETextAlignHorizontal.RIGHT => "[right]" + content + "[/right]",
                _ => content
            };
        }
    }
}
