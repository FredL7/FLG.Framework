using System.Xml;

using FLG.Cs.IDatamodel;


namespace FLG.Cs.UI.Layouts {
    public abstract class AbstractLayoutElementLeaf : AbstractLayoutElement {
        internal AbstractLayoutElementLeaf(string name, XmlNode node)
            : base(name, node) { }
        internal AbstractLayoutElementLeaf(string name, LayoutAttributes attributes)
            : base(name, attributes, false) { }

        public override void AddChild(ILayoutElement child, string id = ILayoutElement.DEFAULT_CHILDREN_TARGET)
        {
            throw new NotImplementedException("Layout element leaf cannot contain childrens");
        }

        public override IEnumerable<string> GetTargets() => Enumerable.Empty<string>();
        public override bool HasChildren(string id = ILayoutElement.DEFAULT_CHILDREN_TARGET) => false;
        public override IEnumerable<ILayoutElement> GetChildrens(string id = ILayoutElement.DEFAULT_CHILDREN_TARGET) => Enumerable.Empty<ILayoutElement>();

        public sealed override void ComputeRectXform() { /* Parent will drive the size */ }
    }
}
