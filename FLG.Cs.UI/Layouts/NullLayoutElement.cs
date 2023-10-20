using FLG.Cs.Math;

namespace FLG.Cs.UI.Layouts {
    internal class NullLayoutElement : AbstractLayoutElementLeaf {
        internal NullLayoutElement() : base(new RectXform(), Size.Zero, 0, 1) { }
    }
}
