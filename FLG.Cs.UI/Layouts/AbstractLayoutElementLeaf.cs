using System.Xml;

using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;

namespace FLG.Cs.UI.Layouts {
    internal class AbstractLayoutElementLeaf : AbstractLayoutElement {
        internal AbstractLayoutElementLeaf(XmlNode node, string name)
            : base(node, name) { }

        internal override void AddChild(AbstractLayoutElement child)
        {
            var logger = SingletonManager.Instance.Get<ILogManager>();
            logger.Error("Layout element leaf cannot contain childrens");
            throw new NotImplementedException();
        }

        public override bool HasChildren() => false;
        public override IEnumerable<AbstractLayoutElement> GetChildrens()
        {
            var logger = SingletonManager.Instance.Get<ILogManager>();
            logger.Error("Layout element leaf cannot contain childrens");
            throw new NotImplementedException();
        }

        internal override void ComputeRectXform() { }
    }
}
