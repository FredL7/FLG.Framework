using FLG.Cs.Math;
using System.Xml;

namespace FLG.Cs.UI.Layouts {
    public abstract class AbstractLayoutElementComposite : AbstractLayoutElement {
        private List<AbstractLayoutElement> _childrens = new();
        protected List<AbstractLayoutElement> GetChildrensInternal() => _childrens;

        internal AbstractLayoutElementComposite(RectXform rectXform, Size size, int order, float stretchWeight)
            : base(rectXform, size, order, stretchWeight) { }

        internal AbstractLayoutElementComposite(XmlNode node) : base(node) { }

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
