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
                PlaceholderText = Widget.Placeholder,
            };

            string value = Widget.Model.GetValueAsString();
            if (value != string.Empty)
                _inputField.Text = value;

            if (!fromEditor)
            {
                _inputField.TextChanged += OnTextChanged;
                Widget.Model.SetResetCallback(Reset);
            }

            return _inputField;
        }

        private void OnTextChanged(string text)
        {
            if (!Widget.Model.SetValue(text))
            {
                // TODO: Error handling
            }
        }

        private void Reset()
        {
            if (_inputField != null)
            {
                _inputField.Text = Widget.Model?.GetValueAsString();
            }
        }
    }
}
