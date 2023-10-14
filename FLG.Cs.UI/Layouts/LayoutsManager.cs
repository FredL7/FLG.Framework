using System.Numerics;

namespace FLG.Cs.UI.Layouts {
    internal class LayoutsManager {
        private Dictionary<uint, AbstractLayout> _layouts;
        private AbstractLayout? _current = null;

        internal LayoutsManager()
        {
            _layouts = new();
        }

        internal void RegisterLayout(uint id, AbstractLayout layout)
        {
            _layouts.Add(id, layout);
        }

        internal void SetLayoutActive(uint id)
        {
            var target = _layouts[id];
            if (target != _current)
            {
                _current?.SetActive(false);
                _current = target;
                _current.SetActive(true);
            }
        }

        internal void ComputeLayoutsRectXforms(Window window)
        {
            foreach (var layout in _layouts)
                layout.Value.ComputeRectXforms(window);
        }
    }
}
