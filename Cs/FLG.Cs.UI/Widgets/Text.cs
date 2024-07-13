using System.Xml;

using FLG.Cs.Datamodel;
using FLG.Cs.UI.Layouts;


namespace FLG.Cs.UI.Widgets {
    internal class Text : AbstractLayoutElementLeaf, IText {
        public override ELayoutElement Type { get => ELayoutElement.TEXT; }

        private string _content;
        public string Content {
            get => _content;
            set {
                _content = value;
                TextChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public ETextAlignHorizontal AlignHorizontal { get; private set; }

        public event EventHandler? TextChanged;

        internal Text(string name, XmlNode node) : base(name, node)
        {
            _content = XMLParser.GetText(node);
            AlignHorizontal = XMLParser.GetTextAlignHorizontal(node);
        }
        internal Text(string name, string content, LayoutAttributes layoutAttr, TextAttributes textAttr)
            :base(name, layoutAttr)
        {
            _content = content;
            AlignHorizontal = textAttr.alignHorizontal;
        }
    }
}
