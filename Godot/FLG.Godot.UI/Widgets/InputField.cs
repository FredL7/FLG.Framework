using Godot;

using FLG.Cs.IDatamodel;


namespace FLG.Godot.UI {
    public class InputField : IWidget<IInputField> {
        public IInputField Widget { get; private set; }

        public InputField(IInputField widget)
        {
            Widget = widget;
        }

        public Node Draw(Node parent, bool fromEditor)
        {
            LineEdit inputField = new()
            {
                Name = Widget.Name,
                Position = new Vector2(Widget.Position.X, Widget.Position.Y),
                Size = new Vector2(Widget.Dimensions.Width, Widget.Dimensions.Height),
                // Alignment = TextAlignmentConverter.Horizontal(Widget.AlignHorizontal)
                Text = Widget.Model.GetValueAsString(),
                PlaceholderText = Widget.Placeholder,
            };

            return inputField;
        }
    }
}
