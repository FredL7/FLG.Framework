using FLG.Cs.Math;
using System.Numerics;
using System.Xml;

namespace FLG.Cs.UI.Layouts {
    internal abstract class AbstractLayoutElement : ILayoutElement {
        internal RectXform RectXform { get; private set; }
        public Vector2 GetPosition() => RectXform.GetContainerPosition();
        public Size GetDimensions() => RectXform.GetDimensions();

        internal Size Size { get; private set; }
        internal int Order { get; private set; }
        internal float Weight { get; private set; }

        /// <summary>
        /// Top-level layout element abstract constructor.
        /// </summary>
        /// <param name="rectXform">Defines the margins and paddings for this element</param>
        /// <param name="size">Expected size (pixels) along the main axis of its container, a value of zero (0) indicates that this element should stretch, used in combination with weight</param>
        /// <param name="order">Defines a custom order for this element</param>
        /// <param name="weight">Combines with a size of zero (0) to define its size, relative to other weights. Priority is given to element that have a size before stretching</param>
        internal AbstractLayoutElement(RectXform rectXform, Size size,  int order, float weight)
        {
            RectXform = rectXform;

            Size = size;
            Order = order;
            Weight = weight;
        }

        //? TODO: Maybe only requires xml constructor and parameter constructor obsolete
        internal AbstractLayoutElement(XmlNode node)
        {
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
