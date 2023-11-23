using System.Xml;

using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;

namespace FLG.Cs.UI.Layouts {
    internal class AbstractLayoutElementLeaf : AbstractLayoutElement {
        internal AbstractLayoutElementLeaf(XmlNode node, string name)
            : base(node, name) { }

        internal override void AddChild(AbstractLayoutElement child)
        {
            LogManager.Instance.Error("Layout element leaf cannot contain childrens");
        }

        public override bool HasChildren() => false;
        public override IEnumerable<AbstractLayoutElement> GetChildrens() => Enumerable.Empty<AbstractLayoutElement>();

        internal sealed override void ComputeRectXform() { }
    }
}
