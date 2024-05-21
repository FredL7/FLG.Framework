using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.UI.Layouts;

namespace FLG.Cs.UI.Widgets {
    internal class Button : AbstractLayoutElementLeaf, IButton {
        public override ELayoutElement Type { get => ELayoutElement.BUTTON; }

        public string Text { get; private set; }
        public Action Action { get; private set; }

        public Button(string name, XmlNode node) : base(name, node)
        {
            // Should not create Button from xml (unless we add reflection on action call)
            throw new NotImplementedException();
        }

        public Button(string name, string text, Action action, LayoutAttributes attributes)
            : base(name, attributes)
        {
            Text = text;
            Action = action;
        }
    }
}
