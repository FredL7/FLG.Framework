using FLG.Cs.Datamodel;
using FLG.Cs.Math;


namespace FLG.Cs.UI.Layouts
{
    internal class LayoutsManager {
        private Dictionary<string, Layout> _layouts;

        private string _currentLayout;

        internal LayoutsManager()
        {
            _layouts = new();
            _currentLayout = string.Empty;
        }

        internal void SetCurrentLayout(string id)
        {
            if (!_layouts.ContainsKey(id))
            {
                throw new Exception($"Layout with id {id} does not exists");
            }

            _currentLayout = id;
        }

        internal Layout GetCurrent()
        {
            if (_currentLayout == string.Empty)
                throw new Exception($"Current layout isn't set.");

            return _layouts[_currentLayout];
        }

        public IEnumerable<ILayout> GetLayouts() => _layouts.Values;
        public ILayout GetLayout(string name) => _layouts[name];

        internal void SetLayoutsFromParser(Dictionary<string, Layout> layouts)
        {
            _layouts = layouts;
        }

        internal void ComputeLayoutsRectXforms(Size windowSize)
        {
            foreach (var layout in _layouts)
                layout.Value.ComputeRectXforms(windowSize);
        }
    }
}
