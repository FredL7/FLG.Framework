using FLG.Cs.Math;
using System.Xml;

namespace FLG.Cs.UI.Layouts {
    internal class ProxyLayoutElementLeaf : AbstractLayoutElementLeaf {
        internal ProxyLayoutElementLeaf(RectXform rectXform, Size size, int order, float stretchWeight) : base(rectXform, size, order, stretchWeight) { }
        internal ProxyLayoutElementLeaf(XmlNode node) : base(node) { }
    }
}
