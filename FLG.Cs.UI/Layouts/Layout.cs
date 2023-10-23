using System.Numerics;

namespace FLG.Cs.UI.Layouts {
    public class Layout : ILayout {
        private bool _active;

        private AbstractLayoutElement _root;
        public ILayoutElement GetRoot() => _root;

        string _name;
        public string GetName() => _name;

        #region Targets
        readonly private List<AbstractLayoutElement> _targets;
        internal void AddTarget(AbstractLayoutElement target) { _targets.Add(target); }
        internal AbstractLayoutElement? GetTarget(string name)
        {
            AbstractLayoutElement? target = null;
            foreach (var element in _targets)
                if (element.GetName() == name)
                    target = element;
            return target;
        }
        #endregion Targets

        #region Observer
        private List<ILayoutObserver> _observers;
        public void AddObserver(ILayoutObserver observer)
        {
            _observers.Add(observer);
        }
        #endregion Observer

        internal Layout(AbstractLayoutElement root, string name)
        {
            _active = false;
            _root = root;
            _targets = new();
            _observers = new();
            _name = name;
        }

        internal void SetActive(bool active)
        {
            if (active && !_active)
            {
                _active = true;
                foreach (var o in _observers)
                    o.OnLayoutSetActive();
            }
            else if (!active && _active)
            {
                _active = false;
                foreach (var o in _observers)
                    o.OnLayoutSetInactive();
            }
        }

        internal void ComputeRectXforms(Window window)
        {
            _root.RectXform.SetSizesAndPosition(window.RectXform.GetDimensions(), Vector2.Zero);
            _root.ComputeRectXform();
        }
    }
}
