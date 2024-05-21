using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.UI.Layouts;


namespace FLG.Cs.UI.Widgets {
    internal class InputField : AbstractLayoutElementLeaf, IInputField {
        public override ELayoutElement Type { get => ELayoutElement.INPUTFIELD; }

        public string Placeholder { get; private set; }
        public string Label { get; private set; }
        public IInputFieldModel Model { get; private set; }

        public InputField(string name, XmlNode node) : base(name, node)
        {
            Label = Placeholder = XMLParser.GetStringAttribute(node, "label", name);
            Placeholder = XMLParser.GetStringAttribute(node, "palceholder", string.Empty);
            Model = new SimpleStringModel();
        }

        public InputField(string name, string label, string placeholder, IInputFieldModel model, LayoutAttributes layoutAttr) : base(name, layoutAttr)
        {
            Label = label;
            Placeholder = placeholder;
            Model = model;
        }
    }
}
