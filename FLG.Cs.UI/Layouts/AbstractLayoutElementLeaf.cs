using FLG.Cs.Math;

namespace FLG.Cs.UI.Layouts {
    public class AbstractLayoutElementLeaf : AbstractLayoutElement {
        public AbstractLayoutElementLeaf(RectXform rectXform, Size size, int order, float stretchWeight)
            : base(rectXform, size, order, stretchWeight) { }

        internal override void ComputeRectXform() { }
    }
}
