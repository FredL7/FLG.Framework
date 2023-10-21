using FLG.Cs.Math;
using System.Numerics;
using System.Xml;

namespace FLG.Cs.UI.Layouts {
    internal abstract class AbstractLayoutElement : ILayoutElement {
        string _name;
        public string GetName() => _name;

        internal RectXform RectXform { get; private set; }
        public Vector2 GetPosition() => RectXform.GetContainerPosition();
        public Size GetDimensions() => RectXform.GetDimensions();

        internal Size Size { get; private set; }
        internal int Order { get; private set; }
        internal float Weight { get; private set; }

        internal AbstractLayoutElement(XmlNode node, string name)
        {
            _name = name;

            var margin = LayoutXMLParser.GetMargin(node);
            var padding = LayoutXMLParser.GetPadding(node);
            var width = LayoutXMLParser.GetWidth(node);
            var height = LayoutXMLParser.GetHeight(node);

            RectXform = new(margin, padding);
            Size = new(width, height);
            Order = LayoutXMLParser.GetOrder(node);
            Weight = LayoutXMLParser.GetWeight(node);
        }

        internal abstract void AddChild(AbstractLayoutElement child);
        internal abstract void ComputeRectXform();
        public abstract bool HasChildren();
        public abstract IEnumerable<ILayoutElement> GetChildrens();
    }
}
