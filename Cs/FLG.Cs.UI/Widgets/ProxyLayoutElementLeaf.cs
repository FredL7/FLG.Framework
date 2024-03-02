using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;


namespace FLG.Cs.UI.Widgets
{
    public class ProxyLayoutElementLeaf : AbstractLayoutElementLeaf
    {
        public override ELayoutElement Type { get => ELayoutElement.PROXY_LAYOUT_ELEMENT; }

        public ProxyLayoutElementLeaf(string name, XmlNode node) : base(name, node) { }
        public ProxyLayoutElementLeaf(string name, float width, float height, Spacing margin, Spacing padding, int order, float weight)
            : base(name, width, height, margin, padding, order, weight) { }
    }
}
