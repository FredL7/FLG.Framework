using System.Numerics;
using System.Xml;

using FLG.Cs.Math;

namespace FLG.Cs.UI.Layouts {
    internal abstract class AbstractLayoutElement : ILayoutElement {
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

        internal AbstractLayoutElement(XmlNode node, string name)
        {
            _name = name;
            _isTarget = XMLParser.GetTarget(node) != string.Empty;

            var margin = XMLParser.GetMargin(node);
            var padding = XMLParser.GetPadding(node);
            var width = XMLParser.GetWidth(node);
            var height = XMLParser.GetHeight(node);

            RectXform = new(margin, padding);
            Size = new(width, height);
            Order = XMLParser.GetOrder(node);
            Weight = XMLParser.GetWeight(node);
        }

        internal abstract void AddChild(AbstractLayoutElement child);
        internal abstract void ComputeRectXform();
        public abstract bool HasChildren();
        public abstract IEnumerable<ILayoutElement> GetChildrens();
    }
}
