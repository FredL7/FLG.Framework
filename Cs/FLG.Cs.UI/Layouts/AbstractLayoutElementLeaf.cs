using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.Math;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.UI.Layouts {
    public abstract class AbstractLayoutElementLeaf : AbstractLayoutElement {
        internal AbstractLayoutElementLeaf(string name, XmlNode node)
            : base(name, node) { }
        internal AbstractLayoutElementLeaf(string name, float width, float height, Spacing margin, Spacing padding, int order, float weight)
            : base(name, width, height, margin, padding, order, weight, false) { }

        public override void AddChild(ILayoutElement child, string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER)
        {
            Locator.Instance.Get<ILogManager>().Error("Layout element leaf cannot contain childrens");
        }

        public override IEnumerable<string> GetContainers() => Enumerable.Empty<string>();
        public override bool HasChildren(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER) => false;
        public override IEnumerable<ILayoutElement> GetChildrens(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER) => Enumerable.Empty<ILayoutElement>();

        public sealed override void ComputeRectXform() { }
    }
}
