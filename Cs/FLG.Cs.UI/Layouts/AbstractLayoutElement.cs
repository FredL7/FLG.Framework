using System.Numerics;
using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.Math;


namespace FLG.Cs.UI.Layouts
{
    public abstract class AbstractLayoutElement : ILayoutElement {
        public string Name { get; private set; }
        public bool IsTarget { get; private set; }
        public RectXform RectXform { get; private set; }
        public Vector2 Position { get => RectXform.GetContainerPosition(); }
        public Size Dimensions { get => RectXform.GetDimensions(); }
        public Size Size { get; private set; }
        public int Order { get; private set; }
        public float Weight { get; private set; }

        internal AbstractLayoutElement(string name, XmlNode node)
        {
            Name = name;
            IsTarget = XMLParser.GetTarget(node) != string.Empty;

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
            Name = name;
            IsTarget = isTarget;
            Order = order;
            Weight = weight;

            RectXform = new(margin, padding);
            Size = new(width, height);
        }

        public abstract void AddChild(ILayoutElement child, string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER);
        public abstract void ComputeRectXform();
        public abstract IEnumerable<string> GetContainers();
        public abstract bool HasChildren(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER);
        public abstract IEnumerable<ILayoutElement> GetChildrens(string id = ILayoutElement.DEFAULT_CHILDREN_CONTAINER);
    }
}
