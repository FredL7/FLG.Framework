using UnityEngine;
using UnityEngine.UI;

using FLG.Cs.Datamodel.UI.Widgets.Text;

using FLG.Unity.Helpers;


namespace FLG.Unity.UI.Widgets {
    internal class FLGLabel(ILabel widget) : IWidget<ILabel> {
        public ILabel Widget { get; private set; } = widget;

        public GameObject Draw(bool _)
        {
            var go = new GameObject
            {
                name = Widget.Name,
            };

            var label = go.AddComponent<Text>();
            label.text = Widget.Text;
            label.alignment = TextAlignmentConverter.ToAnchor(Widget.AlignHorizontal, Widget.AlignVertical);
            label.rectTransform.sizeDelta = new Vector2(Widget.Dimensions.Width, Widget.Dimensions.Height);
            label.rectTransform.position = new Vector2(Widget.Position.X, Widget.Position.Y);

            return go;
        }
    }
}
