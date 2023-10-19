using FLG.Cs.Math;
using System.Xml;

namespace FLG.Cs.UI.Layouts {
    public class AbstractLayoutElementLeaf : AbstractLayoutElement {
        public AbstractLayoutElementLeaf(RectXform rectXform, Size size, int order, float stretchWeight)
            : base(rectXform, size, order, stretchWeight) { }

        public AbstractLayoutElementLeaf(XmlNode node)
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
