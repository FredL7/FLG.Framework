using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.UI.Layouts;


namespace FLG.Cs.UI.Widgets
{
    public class ProxyLayoutElementLeaf : AbstractLayoutElementLeaf
    {
        public override ELayoutElement Type { get => ELayoutElement.PROXY_LAYOUT_ELEMENT; }

        public ProxyLayoutElementLeaf(string name, XmlNode node) : base(name, node) { }
        public ProxyLayoutElementLeaf(string name, LayoutAttributes attributes)
            : base(name, attributes) { }
    }
}
