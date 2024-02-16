using System.Numerics;
using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.Math;


namespace FLG.Cs.UI.Layouts
{
    public abstract class AbstractLayoutElement : ILayoutElement {
        private string _name;
        public string GetName() => _name;

        private bool _isTarget;
        public bool GetIsTarget() => _isTarget;

        internal RectXform RectXform { get; private set; }
        public Vector2 GetPosition() => RectXform.GetContainerPosition();
        public Size GetDimensions() => RectXform.GetDimensions();

        internal Size Size { get; private set; }
        internal int Order { get; private set; }
        internal float Weight { get; private set; }

        internal AbstractLayoutElement(string name, XmlNode node)
        {
            _name = name;
            _isTarget = XMLParser.GetTarget(node) != string.Empty;

            Order = XMLParser.GetOrder(node);
            Weight = XMLParser.GetWeight(node);

            var margin = XMLParser.GetMargin(node);
            var padding = XMLParser.GetPadding(node);
            var width = XMLParser.GetWidth(node);
            var height = XMLParser.GetHeight(node);
            RectXform = new(margin, padding);
            Size = new(width, height);
        }

        public AbstractLayoutElement(string name, float width, float height, Spacing margin, Spacing padding, int order, float weight, bool isTarget)
        {
            _name = name;
            _isTarget = isTarget;
            Order = order;
            Weight = weight;

            RectXform = new(margin, padding);
            Size = new(width, height);
        }

        internal abstract void AddChild(AbstractLayoutElement child, string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER);
        internal abstract void ComputeRectXform();
        public abstract bool HasChildren(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER);
        public abstract IEnumerable<ILayoutElement> GetChildrens(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER);
    }
}
