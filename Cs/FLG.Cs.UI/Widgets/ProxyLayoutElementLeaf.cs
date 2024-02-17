using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;
using System.Xml;


namespace FLG.Cs.UI.Widgets
{
    public class ProxyLayoutElementLeaf : AbstractLayoutElementLeaf
    {
        public ProxyLayoutElementLeaf(string name, XmlNode node) : base(name, node) { }
        public ProxyLayoutElementLeaf(string name, float width, float height, Spacing margin, Spacing padding, int order, float weight, bool isTarget)
            : base(name, width, height, margin, padding, order, weight, isTarget) { }
    }
}
