using Godot;

using FLG.Cs.Datamodel.UI.Widgets.Text;

using FLG.Godot.Helpers;


namespace FLG.Godot.UI.Widgets {
    public class FLGLabel(ILabel widget) : IWidget<ILabel> {
        public ILabel Widget { get; private set; } = widget;

        public Node Draw(Node parent, bool _)
        {
            Label label = new()
            {
                Name = Widget.Name,
                Position = new Vector2(Widget.Position.X, Widget.Position.Y),
                Size = new Vector2(Widget.Dimensions.Width, Widget.Dimensions.Height),
                Text = Widget.Text,
                HorizontalAlignment = TextAlignmentConverter.Horizontal(Widget.AlignHorizontal),
                VerticalAlignment = TextAlignmentConverter.Vertical(Widget.AlignVertical),
            };
            return label;
        }
    }
}
