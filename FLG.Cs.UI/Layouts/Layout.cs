using System.Numerics;

namespace FLG.Cs.UI.Layouts {
    public class Layout : ILayout {
        private bool _active;

        private AbstractLayoutElement _root;
        public ILayoutElement GetRoot() => _root;

        internal Layout(AbstractLayoutElement root)
        {
            _active = false;
            _root = root;
        }

        internal void SetActive(bool active)
        {
            if (active && !_active)
            {
                _active = true;
            }
            else if (!active && _active)
            {
                _active = false;
            }
        }

        // TODO: Add listeners for OnSetActive and OnSetInactive

        internal void ComputeRectXforms(Window window)
        {
            _root.RectXform.SetSizesAndPosition(window.RectXform.GetDimensions(), Vector2.Zero);
            _root.ComputeRectXform();
        }
    }
}
