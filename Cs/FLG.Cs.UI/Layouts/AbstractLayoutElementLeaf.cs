using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.Math;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.UI.Layouts {
    public class AbstractLayoutElementLeaf : AbstractLayoutElement {
        internal AbstractLayoutElementLeaf(string name, XmlNode node)
            : base(name, node) { }
        internal AbstractLayoutElementLeaf(string name, float width, float height, Spacing margin, Spacing padding, int order, float weight, bool isTarget)
            : base(name, width, height, margin, padding, order, weight, isTarget) { }

        internal override void AddChild(AbstractLayoutElement child, string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER)
        {
            Locator.Instance.Get<ILogManager>().Error("Layout element leaf cannot contain childrens");
        }

        public override bool HasChildren(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER) => false;
        public override IEnumerable<AbstractLayoutElement> GetChildrens(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER) => Enumerable.Empty<AbstractLayoutElement>();

        internal sealed override void ComputeRectXform() { }
    }
}
