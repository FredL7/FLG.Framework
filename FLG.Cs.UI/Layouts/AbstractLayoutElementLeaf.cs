using FLG.Cs.Math;
using System.Xml;

namespace FLG.Cs.UI.Layouts {
    internal class AbstractLayoutElementLeaf : AbstractLayoutElement {
        internal AbstractLayoutElementLeaf(RectXform rectXform, Size size, int order, float stretchWeight)
            : base(rectXform, size, order, stretchWeight) { }

        internal AbstractLayoutElementLeaf(XmlNode node)
            : base(node) { }

        internal override void AddChild(AbstractLayoutElement child)
        {
            throw new NotImplementedException();
            // TODO: Better throw of error log
        }

        public override bool HasChildren() => false;
        public override IEnumerable<AbstractLayoutElement> GetChildrens()
        {
            throw new NotImplementedException();
            // TODO: Better throw of error log
        }

        internal override void ComputeRectXform() { }
    }
}
