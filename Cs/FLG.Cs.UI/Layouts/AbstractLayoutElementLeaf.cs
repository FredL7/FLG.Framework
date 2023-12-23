using System.Xml;

using FLG.Cs.Logger;
using FLG.Cs.Math;
using FLG.Cs.ServiceLocator;

namespace FLG.Cs.UI.Layouts {
    internal class AbstractLayoutElementLeaf : AbstractLayoutElement {
        internal AbstractLayoutElementLeaf(string name, XmlNode node)
            : base(name, node) { }
        internal AbstractLayoutElementLeaf(string name, float width, float height, Spacing margin, Spacing padding, int order, float weight, bool isTarget)
            : base(name, width, height, margin, padding, order, weight, isTarget) { }

        internal override void AddChild(AbstractLayoutElement child)
        {
            Locator.Instance.Get<ILogManager>().Error("Layout element leaf cannot contain childrens");
        }

        public override bool HasChildren() => false;
        public override IEnumerable<AbstractLayoutElement> GetChildrens() => Enumerable.Empty<AbstractLayoutElement>();

        internal sealed override void ComputeRectXform() { }
    }
}
