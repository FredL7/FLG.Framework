using Godot;

using FLG.Cs.Datamodel.UI.Widgets.Forms;


namespace FLG.Godot.UI.Widgets {
    public class FLGInputField(IInputField widget) : IWidget<IInputField> {
        public IInputField Widget { get; private set; } = widget;

        private LineEdit? _inputField;

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
