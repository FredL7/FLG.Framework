using System.Xml;

using FLG.Cs.Math;

namespace FLG.Cs.UI.Layouts {
    internal abstract class AbstractLayoutElementComposite : AbstractLayoutElement {
        private List<AbstractLayoutElement> _childrens = new();
        protected List<AbstractLayoutElement> GetChildrensInternal() => _childrens;

        internal AbstractLayoutElementComposite(XmlNode node, string name)
            : base(node, name) { }

        internal override void AddChild(AbstractLayoutElement child)
        {
            _childrens.Add(child);
        }

        public override bool HasChildren() => _childrens.Count > 0;
        public override IEnumerable<AbstractLayoutElement> GetChildrens() => _childrens;

        internal override void ComputeRectXform()
        {
            ComputeChildrenSizesAndPositions(RectXform.GetDimensions());
            foreach (var child in _childrens)
                child.ComputeRectXform();
        }
        protected abstract void ComputeChildrenSizesAndPositions(Size parentDimensions);
    }
}
