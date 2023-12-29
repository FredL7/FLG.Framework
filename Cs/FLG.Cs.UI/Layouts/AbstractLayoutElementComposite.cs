using FLG.Cs.Math;
using System.Xml;


namespace FLG.Cs.UI.Layouts {
    internal abstract class AbstractLayoutElementComposite : AbstractLayoutElement {
        private readonly Dictionary<string, List<AbstractLayoutElement>> _childrens = new();
        protected List<AbstractLayoutElement> GetChildrensInternal(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER) => _childrens[id];

        internal AbstractLayoutElementComposite(string name, XmlNode node)
            : base(name, node)
        {
            SetupDefaultChildrensContainer();
        }

        internal AbstractLayoutElementComposite(string name, float width, float height, Spacing margin, Spacing padding, int order, float weight, bool isTarget)
            : base(name, width, height, margin, padding, order, weight, isTarget)
        {
            SetupDefaultChildrensContainer();
        }

        private void SetupDefaultChildrensContainer()
        {
            _childrens.Add(ILayoutElement.DEFAULT_CHILDREN_CONTAINER, new());
        }

        internal override void AddChild(AbstractLayoutElement child, string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER)
        {
            if (!_childrens.ContainsKey(id))
                _childrens.Add(id, new());
            _childrens[id].Add(child);
        }

        public override bool HasChildren(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER) => _childrens[id].Count > 0;
        public override IEnumerable<AbstractLayoutElement> GetChildrens(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER) => _childrens[id];

        internal sealed override void ComputeRectXform(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER)
        {
            ComputeChildrenSizesAndPositions();
            foreach (var child in _childrens[id])
                child.ComputeRectXform(id);
        }
        protected abstract void ComputeChildrenSizesAndPositions();
        internal abstract void ComputeContentSizesAndPositions(List<AbstractLayoutElement> content);
    }
}
