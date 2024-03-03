using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.UI.Layouts;

namespace FLG.Cs.UI.Widgets {
    internal class Text : AbstractLayoutElementLeaf, IText {
        public override ELayoutElement Type { get => ELayoutElement.TEXT; }

        public string Value { get; private set; }

        public Text(string name, XmlNode node) : base(name, node)
        {
            Value = XMLParser.GetText(node);
        }
        public Text(string name, string value, LayoutAttributes attributes)
            :base(name, attributes)
        {
            Value = value;
        }
    }
}
