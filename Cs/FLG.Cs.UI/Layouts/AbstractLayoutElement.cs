using System.Numerics;
using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.Math;


namespace FLG.Cs.UI.Layouts
{
    public abstract class AbstractLayoutElement : ILayoutElement {
        private string _name;
        private bool _isTarget;
        internal RectXform _rectXform;
        internal Size _size;
        internal int _order;
        internal float _weight;

        public string GetName() => _name;
        public bool GetIsTarget() => _isTarget;
        public RectXform GetRectXform() => _rectXform;
        public Vector2 GetPosition() => _rectXform.GetContainerPosition();
        public Size GetDimensions() => _rectXform.GetDimensions();
        public Size GetSize() => _size;
        public int GetOrder() => _order;
        public float GetWeight() => _weight;

        internal AbstractLayoutElement(string name, XmlNode node)
        {
            _name = name;
            _isTarget = XMLParser.GetTarget(node) != string.Empty;

            _order = XMLParser.GetOrder(node);
            _weight = XMLParser.GetWeight(node);

            var margin = XMLParser.GetMargin(node);
            var padding = XMLParser.GetPadding(node);
            var width = XMLParser.GetWidth(node);
            var height = XMLParser.GetHeight(node);
            _rectXform = new(margin, padding);
            _size = new(width, height);
        }

        public AbstractLayoutElement(string name, float width, float height, Spacing margin, Spacing padding, int order, float weight, bool isTarget)
        {
            _name = name;
            _isTarget = isTarget;
            _order = order;
            _weight = weight;

            _rectXform = new(margin, padding);
            _size = new(width, height);
        }

        public abstract void AddChild(ILayoutElement child, string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER);
        public abstract void ComputeRectXform();
        public abstract IEnumerable<string> GetContainers();
        public abstract bool HasChildren(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER);
        public abstract IEnumerable<ILayoutElement> GetChildrens(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER);
    }
}
