using FLG.Cs.Math;
using System.Xml;

namespace FLG.Cs.UI.Layouts {
    internal class AbstractLayoutElementLeaf : AbstractLayoutElement {
        internal AbstractLayoutElementLeaf(XmlNode node, string name)
            : base(node, name) { }

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
