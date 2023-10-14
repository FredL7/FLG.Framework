using FLG.Cs.Math;

namespace FLG.Cs.UI.Layouts {
    public abstract class AbstractLayoutElement {
        public RectXform RectXform { get; private set; }

        internal float Size { get; private set; }
        internal int Order { get; private set; }
        internal float Weight { get; private set; }

        internal AbstractLayoutElement(float size, int order, float weight)
        {
            RectXform = new();

            Size = size;
            Order = order;
            Weight = weight;
        }

        internal abstract void ComputeRectXform();
    }
}
