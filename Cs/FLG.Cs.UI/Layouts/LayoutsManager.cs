using FLG.Cs.IDatamodel;


namespace FLG.Cs.UI.Layouts
{
    internal class LayoutsManager {
        private Dictionary<string, Layout> _layouts;

        internal LayoutsManager()
        {
            _layouts = new();
        }

        internal void setLayoutsFromParser(Dictionary<string, Layout> layouts)
        {
            _layouts = layouts;
        }

        public IEnumerable<ILayout> GetLayouts() => _layouts.Values;

        internal void ComputeLayoutsRectXforms(Window window)
        {
            foreach (var layout in _layouts)
                layout.Value.ComputeRectXforms(window);
        }
    }
}
