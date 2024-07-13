using System.Numerics;
using System.Xml;

using FLG.Cs.Datamodel;
using FLG.Cs.Math;


namespace FLG.Cs.UI.Layouts
{
    public abstract class AbstractLayoutElement : ILayoutElement {
        public string Name { get; private set; }
        public abstract ELayoutElement Type { get; }
        public bool IsTarget { get; private set; }
        public RectXform RectXform { get; private set; }
        public Vector2 Position { get => RectXform.GetWrapperPosition(); }
        public Size Dimensions { get => RectXform.GetDimensions(); }
        public Size Size { get; private set; }
        public int Order { get; private set; }
        public float Weight { get; private set; }
        public string BackgroundImage { get; private set; }

        internal AbstractLayoutElement(string name, XmlNode node, bool isTarget)
        {
            Name = name;
            IsTarget = isTarget;

            Order = XMLParser.GetOrder(node);
            Weight = XMLParser.GetWeight(node);

            var margin = XMLParser.GetMargin(node);
            var padding = XMLParser.GetPadding(node);
            var width = XMLParser.GetWidth(node);
            var height = XMLParser.GetHeight(node);
            RectXform = new(margin, padding);
            Size = new(width, height);

            BackgroundImage = XMLParser.GetStringAttribute(node, "backgroundImage");
        }

        internal AbstractLayoutElement(string name, LayoutAttributes attributes, bool isTarget)
        {
            Name = name;
            IsTarget = isTarget;
            Order = attributes.order;
            Weight = attributes.weight;

            RectXform = new(attributes.margin, attributes.padding);
            Size = new(attributes.width, attributes.height);

            BackgroundImage = attributes.backgroundImage;
        }

        public abstract void AddChild(ILayoutElement child, string id = ILayoutElement.DEFAULT_CHILDREN_TARGET);
        public virtual void OnAddedToPage(string id) { }
        public abstract void ComputeRectXform();
        public abstract IEnumerable<string> GetTargets();
        public abstract bool HasChildren(string id = ILayoutElement.DEFAULT_CHILDREN_TARGET);
        public abstract IEnumerable<ILayoutElement> GetChildrens(string id = ILayoutElement.DEFAULT_CHILDREN_TARGET);
    }
}
