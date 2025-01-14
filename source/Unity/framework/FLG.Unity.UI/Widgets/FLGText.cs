using UnityEngine;
using UnityEngine.UI;

using FLG.Cs.Datamodel.UI.Widgets.Text;
using FLG.Unity.Helpers;


namespace FLG.Unity.UI.Widgets {
    internal class FLGText(IText widget) : IWidget<IText> {
        public IText Widget { get; private set; } = widget;

        private Text? _text = null;

        public GameObject Draw(bool fromEditor)
        {
            var go = new GameObject
            {
                name = Widget.Name,
            };

            _text = go.AddComponent<Text>();
            _text.text = Widget.Content;
            _text.alignment = TextAlignmentConverter.ToAnchor(Widget.AlignHorizontal, Widget.AlignVertical);
            _text.rectTransform.sizeDelta = new Vector2(Widget.Dimensions.Width, Widget.Dimensions.Height);
            _text.rectTransform.position = new Vector2(Widget.Position.X, Widget.Position.Y);

            if (!fromEditor)
            {
                Widget.TextChanged += UpdateText;

            }

            return go;
        }

        public void UpdateText(object? sender, EventArgs e)
        {
            if (_text != null)
            {
                _text.text = Widget.Content;
            }
        }
    }
}
