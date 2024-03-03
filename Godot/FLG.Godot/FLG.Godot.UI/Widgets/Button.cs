using Godot;

using FLG.Cs.IDatamodel;


using gd_Button = Godot.Button;


namespace FLG.Godot.UI.Widgets {
    internal class Button : IWidget<IButton> {
        public IButton Widget { get; private set; }

        public Button(IButton widget)
        {
            Widget = widget;
        }

        public Node Draw(Node parent, Node root, bool fromEditor)
        {
            gd_Button btn = new()
            {
                Name = Widget.Name + " button",
                Position = new Vector2(Widget.Position.X, Widget.Position.Y),
                Size = new Vector2(Widget.Dimensions.Width, Widget.Dimensions.Height),
                Text = Widget.Text,
            };

            if (!fromEditor)
            {
                btn.Pressed += Widget.Action;
            }
            parent.AddChild(btn);
            btn.Owner = root;
            return btn;
        }
    }
}
