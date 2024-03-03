using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;


namespace FLG.Cs.UI.Widgets {
    internal class Label : AbstractLayoutElementLeaf, ILabel {
        public override ELayoutElement Type { get => ELayoutElement.LABEL; }

        public string Text { get; private set; }

        public Label(string name, XmlNode node) : base(name, node)
        {
            Text = XMLParser.GetText(node);
        }
        public Label(string name, string text, LayoutAttributes attributes)
            : base(name, attributes)
        {
            Text = text;
        }
    }
}
