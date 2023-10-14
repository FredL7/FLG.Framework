using FLG.Cs.Math;
using System.Numerics;

namespace FLG.Cs.UI.Layouts {
    internal class AbstractLayoutElementLeaf : AbstractLayoutElement {
        internal AbstractLayoutElementLeaf(float size, int order, float stretchWeight) : base(size, order, stretchWeight) { }

        internal override void ComputeRectXform() { }
    }
}
