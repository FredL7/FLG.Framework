using Godot;

using FLG.Cs.IDatamodel;


using gd_Label = Godot.Label;


namespace FLG.Godot.UI.Widgets {
    internal class Label : IWidget<ILabel> {
        public ILabel Widget { get; private set; }

        public Label(ILabel widget) {
            Widget = widget;
        }

        public Node Draw(Node parent, Node root, bool _)
        {
            gd_Label label = new()
            {
                Name = Widget.Name + " label",
                Position = new Vector2(Widget.Position.X, Widget.Position.Y),
                Size = new Vector2(Widget.Dimensions.Width, Widget.Dimensions.Height),
                Text = Widget.Text,
            };
            parent.AddChild(label);
            label.Owner = root;
            return label;
        }
    }
}
