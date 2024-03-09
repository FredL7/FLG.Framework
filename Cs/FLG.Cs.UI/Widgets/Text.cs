﻿using System.Xml;

using FLG.Cs.IDatamodel;
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

        public event EventHandler? TextChanged;

        public Text(string name, XmlNode node) : base(name, node)
        {
            _content = XMLParser.GetText(node);
        }
        public Text(string name, string content, LayoutAttributes attributes)
            :base(name, attributes)
        {
            _content = content;
        }
    }
}