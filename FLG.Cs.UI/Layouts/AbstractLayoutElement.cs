using FLG.Cs.Math;
using System.Numerics;

namespace FLG.Cs.UI.Layouts {
    public abstract class AbstractLayoutElement {
        public RectXform RectXform { get; private set; }

        internal Size Size { get; private set; }
        internal int Order { get; private set; }
        internal float Weight { get; private set; }

        /// <summary>
        /// Top-level layout element abstract constructor.
        /// </summary>
        /// <param name="rectXform">Defines the margins and paddings for this element</param>
        /// <param name="size">Expected size (pixels) along the main axis of its container, a value of zero (0) indicates that this element should stretch, used in combination with weight</param>
        /// <param name="order">Defines a custom order for this element</param>
        /// <param name="weight">Combines with a size of zero (0) to define its size, relative to other weights. Priority is given to element that have a size before stretching</param>
        internal AbstractLayoutElement(RectXform rectXform, Size size,  int order, float weight = 0)
        {
            RectXform = rectXform;

            Size = size;
            Order = order;
            Weight = weight;
        }

        internal abstract void ComputeRectXform();
    }
}
