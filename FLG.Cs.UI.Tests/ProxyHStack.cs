using FLG.Cs.Math;
using FLG.Cs.UI.Grid;

namespace FLG.Cs.UI.Tests {
    internal class ProxyHStack : HStack {
        public ProxyHStack(RectXform rectXform, Size size, int order = 0, float stretchWeight = 1) : base(rectXform, size, order, stretchWeight) { }
    }
}
