using System.Xml;

using FLG.Cs.Datamodel;
using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;


namespace FLG.Cs.UI.Widgets {
    internal class Sprite : AbstractLayoutElementLeaf, ISprite {
        public override ELayoutElement Type { get => ELayoutElement.SPRITE; }

        public string Source { get; private set; }

        internal Sprite(string name, XmlNode node) : base(name, node)
        {
            Source = XMLParser.GetStringAttribute(node, "source", string.Empty);
            // TODO: Warn if Source is empty or fallback to magenta cube :) ?
            // TODO: Allow no width or height declaration and instead fill parent while keeping ratio
        }
        internal Sprite(string name, string source, LayoutAttributes attributes)
            : base(name, attributes)
        {
            Source = source;
        }
    }
}
