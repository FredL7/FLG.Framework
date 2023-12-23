using FLG.Cs.Math;
using System.Xml;

namespace FLG.Cs.UI.Layouts {
    internal abstract class AbstractLayoutElementComposite : AbstractLayoutElement {
        private List<AbstractLayoutElement> _childrens = new();
        protected List<AbstractLayoutElement> GetChildrensInternal() => _childrens;

        internal AbstractLayoutElementComposite(string name, XmlNode node)
            : base(name, node) { }
        internal AbstractLayoutElementComposite(string name, float width, float height, Spacing margin, Spacing padding, int order, float weight, bool isTarget)
            : base(name, width, height, margin, padding, order, weight, isTarget) { }

        internal override void AddChild(AbstractLayoutElement child)
        {
            _childrens.Add(child);
        }

        public override bool HasChildren() => _childrens.Count > 0;
        public override IEnumerable<AbstractLayoutElement> GetChildrens() => _childrens;

        internal sealed override void ComputeRectXform()
        {
            ComputeChildrenSizesAndPositions();
            foreach (var child in _childrens)
                child.ComputeRectXform();
        }
        protected abstract void ComputeChildrenSizesAndPositions();
        internal abstract void ComputeContentSizesAndPositions(List<AbstractLayoutElement> content);
    }
}
