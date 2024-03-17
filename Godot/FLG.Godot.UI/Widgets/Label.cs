using Godot;

using FLG.Cs.IDatamodel;


using gd_Label = Godot.Label;


namespace FLG.Godot.UI {
    public class Label : IWidget<ILabel> {
        public ILabel Widget { get; private set; }

        public Label(ILabel widget)
        {
            Widget = widget;
        }

        public Node Draw(Node parent, bool _)
        {
            gd_Label label = new()
            {
                Name = Widget.Name,
                Position = new Vector2(Widget.Position.X, Widget.Position.Y),
                Size = new Vector2(Widget.Dimensions.Width, Widget.Dimensions.Height),
                Text = Widget.Text,
            };
            return label;
        }
    }
}
