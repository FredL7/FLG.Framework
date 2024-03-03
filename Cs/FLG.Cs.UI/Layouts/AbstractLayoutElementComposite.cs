using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.Math;


namespace FLG.Cs.UI.Layouts {
    public abstract class AbstractLayoutElementComposite : AbstractLayoutElement {
        private readonly Dictionary<string, List<ILayoutElement>> _childrens = new();
        protected List<ILayoutElement> GetChildrensInternal(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER) => _childrens[id];

        internal AbstractLayoutElementComposite(string name, XmlNode node)
            : base(name, node)
        {
            SetupDefaultChildrensContainer();
        }

        internal AbstractLayoutElementComposite(string name, LayoutAttributes attributes, bool isTarget)
            : base(name, attributes, isTarget)
        {
            SetupDefaultChildrensContainer();
        }

        private void SetupDefaultChildrensContainer()
        {
            _childrens.Add(ILayoutElement.DEFAULT_CHILDREN_CONTAINER, new());
        }

        public override void AddChild(ILayoutElement child, string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER)
        {
            if (!_childrens.ContainsKey(id))
                _childrens.Add(id, new());
            _childrens[id].Add(child);
        }

        public override IEnumerable<string> GetContainers() => _childrens.Keys;
        public override bool HasChildren(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER) => _childrens[id].Count > 0;
        public override IEnumerable<ILayoutElement> GetChildrens(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER) => _childrens[id];

        public sealed override void ComputeRectXform()
        {
            foreach (var container in _childrens)
            {
                ComputeChildrenSizesAndPositions(container.Key);
                foreach (var child in container.Value)
                    child.ComputeRectXform();
            }
        }
        protected abstract void ComputeChildrenSizesAndPositions(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER);
        internal abstract void ComputeContentSizesAndPositions(List<ILayoutElement> content);
    }
}
