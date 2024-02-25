using Godot;

using FLG.Cs.IDatamodel;


using gd_Label = Godot.Label;


namespace FLG.Godot.UI.Widgets {
    internal class Label : IWidget<ILabel> {
        public ILabel Widget { get; private set; }

        public Label(ILabel widget) {
            Widget = widget;
        }

        public void Draw(Node node, Node root)
        {
            gd_Label label = new()
            {
                Name = node.Name + " label",
                Size = new Vector2(Widget.Dimensions.Width, Widget.Dimensions.Height),
                Text = Widget.Text,
            };
            node.AddChild(label);
            label.Owner = root;
        }
    }
}
