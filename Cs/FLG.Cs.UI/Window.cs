using System.Numerics;

using FLG.Cs.Math;


namespace FLG.Cs.UI {
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
