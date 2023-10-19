using FLG.Cs.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FLG.Cs.UI.Layouts {
    internal class ProxyLayoutElementLeaf : AbstractLayoutElementLeaf {
        public ProxyLayoutElementLeaf(RectXform rectXform, Size size, int order, float stretchWeight) : base(rectXform, size, order, stretchWeight) { }
        public ProxyLayoutElementLeaf(XmlNode node) : base(node) { }
    }
}
