﻿using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;


namespace FLG.Cs.UI.Widgets {
    internal class Sprite : AbstractLayoutElementLeaf, ISprite {
        public override ELayoutElement Type { get => ELayoutElement.SPRITE; }

        public string Source { get; private set; }

        public Sprite(string name, XmlNode node) : base(name, node)
        {
            Source = XMLParser.GetStringAttribute(node, "source", string.Empty);
        }
        public Sprite(string name, string source, LayoutAttributes attributes)
            : base(name, attributes)
        {
            Source = source;
        }
    }
}
