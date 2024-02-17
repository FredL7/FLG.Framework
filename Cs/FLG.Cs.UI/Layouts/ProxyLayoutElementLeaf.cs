using FLG.Cs.Math;
using System.Xml;


namespace FLG.Cs.UI.Layouts {
    public class ProxyLayoutElementLeaf : AbstractLayoutElementLeaf {
        internal ProxyLayoutElementLeaf(string name, XmlNode node) : base(name, node) { }
        internal ProxyLayoutElementLeaf(string name, float width, float height, Spacing margin, Spacing padding, int order, float weight, bool isTarget)
            : base(name, width, height, margin, padding, order, weight, isTarget) { }
    }
}
