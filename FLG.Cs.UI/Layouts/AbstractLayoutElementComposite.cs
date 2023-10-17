using FLG.Cs.Math;

namespace FLG.Cs.UI.Layouts {
    public abstract class AbstractLayoutElementComposite : AbstractLayoutElement {
        private List<AbstractLayoutElement> _childrens;
        protected List<AbstractLayoutElement> GetChildrens() => _childrens;

        internal AbstractLayoutElementComposite(RectXform rectXform, Size size, int order = 0, float stretchWeight = 1)
            : base(rectXform, size, order, stretchWeight)
        {
            _childrens = new();
        }

        public void AddChild(AbstractLayoutElement child)
        {
            _childrens.Add(child);
        }

        internal override void ComputeRectXform()
        {
            ComputeChildrenSizesAndPositions(RectXform.GetDimensions());
            foreach (var child in _childrens)
                child.ComputeRectXform();
        }
        protected abstract void ComputeChildrenSizesAndPositions(Size parentDimensions);
    }
}
