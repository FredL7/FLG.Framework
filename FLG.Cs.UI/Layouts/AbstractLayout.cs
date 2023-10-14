using System.Numerics;

namespace FLG.Cs.UI.Layouts {
    public abstract class AbstractLayout {
        private ELayoutStatus _status;

        private AbstractLayoutElement _root;

        public AbstractLayout(AbstractLayoutElement root)
        {
            _status = ELayoutStatus.INACTIVE;
            _root = root;
        }

        internal void SetActive(bool active)
        {
            if (active && _status == ELayoutStatus.INACTIVE)
            {
                _status = ELayoutStatus.ACTIVE;
                OnSetActive();
            }
            else if (!active && _status == ELayoutStatus.ACTIVE)
            {
                _status = ELayoutStatus.INACTIVE;
                OnSetInactive();
            }
        }
        protected void OnSetActive() { }
        protected void OnSetInactive() { }
        internal void ComputeRectXforms(Window window)
        {
            _root.RectXform.SetSizesAndPosition(window.RectXform.GetDimensions(), Vector2.Zero);
            _root.ComputeRectXform();
        }
    }
}
