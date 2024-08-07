﻿using System.Xml;

using FLG.Cs.Datamodel;


namespace FLG.Cs.UI.Layouts {
    public abstract class AbstractLayoutElementComposite : AbstractLayoutElement {
        private readonly Dictionary<string, List<ILayoutElement>> _childrens = new();
        protected List<ILayoutElement> GetChildrensInternal(string id = ILayoutElement.DEFAULT_CHILDREN_TARGET)
        {
            CreateChildrenContainerIfDoesntExists(id);
            return _childrens[id]; ;
        }

        internal AbstractLayoutElementComposite(string name, XmlNode node)
            : base(name, node, true)
        {
            SetupDefaultChildrensTarget();
        }

        internal AbstractLayoutElementComposite(string name, LayoutAttributes attributes)
            : base(name, attributes, true)
        {
            SetupDefaultChildrensTarget();
        }

        private void SetupDefaultChildrensTarget()
        {
            _childrens.Add(ILayoutElement.DEFAULT_CHILDREN_TARGET, new());
        }

        private void CreateChildrenContainerIfDoesntExists(string id)
        {
            if (!_childrens.ContainsKey(id))
                _childrens.Add(id, new());
        }

        public override void AddChild(ILayoutElement child, string id = ILayoutElement.DEFAULT_CHILDREN_TARGET)
        {
            CreateChildrenContainerIfDoesntExists(id);
            _childrens[id].Add(child);

            child.OnAddedToPage(id);
        }

        public override IEnumerable<string> GetTargets() => _childrens.Keys;
        public override bool HasChildren(string id = ILayoutElement.DEFAULT_CHILDREN_TARGET)
        {
            if (!_childrens.ContainsKey(id))
                return false;
            return _childrens[id].Count > 0;
        }
        public override IEnumerable<ILayoutElement> GetChildrens(string id = ILayoutElement.DEFAULT_CHILDREN_TARGET) => _childrens[id];

        public sealed override void ComputeRectXform()
        {
            foreach (var target in _childrens)
            {
                if (target.Value.Count > 0)
                {
                    ComputeChildrenSizesAndPositions(target.Key);
                    foreach (var child in target.Value)
                        child.ComputeRectXform();
                }
            }
        }
        protected abstract void ComputeChildrenSizesAndPositions(string id = ILayoutElement.DEFAULT_CHILDREN_TARGET);
        internal abstract void ComputeContentSizesAndPositions(List<ILayoutElement> content);
    }
}
