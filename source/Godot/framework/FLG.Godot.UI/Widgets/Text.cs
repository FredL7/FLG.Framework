using Godot;

using FLG.Cs.Datamodel.UI.Widgets.Text;


namespace FLG.Godot.UI.Widgets {
    public class FLGText(IText widget) : IWidget<IText> {
        public IText Widget { get; private set; } = widget;

        private RichTextLabel? label;

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

        public void UpdateText(object? sender, EventArgs e)
        {
            if (label != null)
            {
                label.Text = TextAlign(Widget.Content);
            }
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
