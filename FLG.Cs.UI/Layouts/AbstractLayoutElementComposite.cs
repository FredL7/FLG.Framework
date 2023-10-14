using FLG.Cs.Math;
using System.Numerics;

namespace FLG.Cs.UI.Layouts {
    internal abstract class AbstractLayoutElementComposite : AbstractLayoutElement {
        List<AbstractLayoutElement> _childrens;

        internal AbstractLayoutElementComposite(float size, int order, float stretchWeight) : base(size, order, stretchWeight)
        {
            _childrens = new();
        }

        internal void AddChild(AbstractLayoutElement child)
        {
            _childrens.Add(child);
        }

        internal override void ComputeRectXform()
        {
            ComputeChildrenSizesAndPositions(RectXform.GetDimensions());
            foreach (var child in _childrens)
                child.ComputeRectXform();
        }
        internal abstract void ComputeChildrenSizesAndPositions(Size parentDimensions);
        // TODO: for each children: children.SetSizesAndPosition(Size computedBounds, Vector2 computedPosition);
    }
}
