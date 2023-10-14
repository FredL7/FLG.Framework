using FLG.Cs.Math;
using System.Numerics;

namespace FLG.Cs.UI.Layouts {
    internal class Window {
        internal RectXform RectXform { get; private set; }

        internal Window(float width, float height)
        {
            RectXform = new(
                margin: Spacing.Zero,
                padding: Spacing.Zero
            );
            RectXform.SetSizesAndPosition(new Size(width, height), Vector2.Zero);
        }
    }
}
