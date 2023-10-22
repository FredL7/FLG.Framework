using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Pages;

namespace FLG.Cs.UI.Layouts {
    internal class LayoutsManager {
        private Dictionary<string, Layout> _layouts;
        private Layout? _current = null;

        internal LayoutsManager()
        {
            _layouts = new();
        }

        public IEnumerable<ILayout> GetLayouts() => _layouts.Values;

        internal void RegisterLayouts(string layoutsDir, Window window)
        {
            var layouts = LayoutXMLParser.Parse(layoutsDir);
            if (layouts != null)
                _layouts = layouts;
            ComputeLayoutsRectXforms(window);
        }

        internal void SetLayoutActive(string id)
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
