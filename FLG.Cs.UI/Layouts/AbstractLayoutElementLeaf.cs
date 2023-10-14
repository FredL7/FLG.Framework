using FLG.Cs.Math;

namespace FLG.Cs.UI.Layouts {
    internal class AbstractLayoutElementLeaf : AbstractLayoutElement {
        internal AbstractLayoutElementLeaf(RectXform rectXform, Size size, int order, float stretchWeight)
            : base(rectXform, size, order, stretchWeight) { }

        internal override void ComputeRectXform() { }
    }
}
