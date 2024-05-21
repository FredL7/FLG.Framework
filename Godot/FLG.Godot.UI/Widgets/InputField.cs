using Godot;

using FLG.Cs.Datamodel;


namespace FLG.Godot.UI {
    public class InputField : IWidget<IInputField> {
        public IInputField Widget { get; private set; }

        private LineEdit? _inputField;

        public InputField(IInputField widget)
        {
            Widget = widget;
        }

        public Node Draw(Node parent, bool fromEditor)
        {
            _inputField = new()
            {
                Name = Widget.Name,
                Position = new Vector2(Widget.Position.X, Widget.Position.Y),
                Size = new Vector2(Widget.Dimensions.Width, Widget.Dimensions.Height),
                // Alignment = TextAlignmentConverter.Horizontal(Widget.AlignHorizontal)
                Text = Widget.Model.GetValueAsString(),
                PlaceholderText = Widget.Placeholder,
            };

            if (!fromEditor)
            {
                _inputField.TextChanged += OnTextChanged;
                Widget.Model.SetClearUICallback(Clear);
            }

            return _inputField;
        }

        private void OnTextChanged(string text)
        {
            Widget.Model.SetValue(text);
        }

        private void Clear()
        {
            _inputField?.Clear();
        }
    }
}
