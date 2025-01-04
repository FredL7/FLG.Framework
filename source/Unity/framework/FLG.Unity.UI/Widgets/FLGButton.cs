using UnityEngine;
using UnityEngine.UI;

using FLG.Cs.Datamodel.UI.Widgets;


namespace FLG.Unity.UI.Widgets {
    internal class FLGButton(IButton widget) : IWidget<IButton> {
        public IButton Widget { get; private set; } = widget;

        public GameObject Draw(bool fromEditor)
        {
            var go = new GameObject()
            {
                name = Widget.Name,
            };

            var button = go.AddComponent<Button>();
            // TODO: Text
            button.transform.position = new Vector2(Widget.Position.X, Widget.Position.Y);
            ((RectTransform)button.transform).sizeDelta = new Vector2(Widget.Dimensions.Width, Widget.Dimensions.Height);

            if (!fromEditor)
            {
                button.onClick.AddListener(() => Widget.Action());
            }

            return go;
        }
    }
}
