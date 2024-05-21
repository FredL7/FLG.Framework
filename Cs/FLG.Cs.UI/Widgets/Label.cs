using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.UI.Layouts;


namespace FLG.Cs.UI.Widgets {
    internal class Label : AbstractLayoutElementLeaf, ILabel {
        public override ELayoutElement Type { get => ELayoutElement.LABEL; }

        public string Text { get; private set; }
        public ETextAlignHorizontal AlignHorizontal { get; private set; }
        public ETextAlignVertical AlignVertical { get; private set; }

        public Label(string name, XmlNode node) : base(name, node)
        {
            Text = XMLParser.GetText(node);
            AlignHorizontal = XMLParser.GetTextAlignHorizontal(node);
            AlignVertical = XMLParser.GetTextAlignVertical(node);
        }
        public Label(string name, string text, LayoutAttributes layoutAttr, TextAttributes textAttr)
            : base(name, layoutAttr)
        {
            Text = text;
            AlignHorizontal = textAttr.alignHorizontal;
            AlignVertical = textAttr.alignVertical;
        }
    }
}
