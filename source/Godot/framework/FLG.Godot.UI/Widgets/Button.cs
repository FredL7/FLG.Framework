using Godot;

using FLG.Cs.Datamodel.UI.Widgets;


namespace FLG.Godot.UI.Widgets {
    public class FLGButton(IButton widget) : IWidget<IButton> {
        public IButton Widget { get; private set; } = widget;

        public Node Draw(Node parent, bool fromEditor)
        {
            Button btn = new()
            {
                Name = Widget.Name,
                Position = new Vector2(Widget.Position.X, Widget.Position.Y),
                Size = new Vector2(Widget.Dimensions.Width, Widget.Dimensions.Height),
                Text = Widget.Text,
            };

            if (!fromEditor)
            {
                btn.Pressed += Widget.Action;
            }

            return btn;
        }
    }
}
