using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.UI.Layouts {
    internal class LayoutsManager {
        private string _layoutsDir;
        private Dictionary<string, Layout> _layouts;
        private Layout? _current = null;

        internal LayoutsManager(string layoutsDir)
        {
            _layoutsDir = layoutsDir;
            _layouts = new();
        }

        public IEnumerable<ILayout> GetLayouts() => _layouts.Values;

        internal void RegisterLayouts(Window window)
        {
            XMLParser parser = new(_layoutsDir);
            var result = parser.Parse();
            if (!result) result.Log();

            _layouts = parser.GetPages();
            ComputeLayoutsRectXforms(window);
        }

        internal void ComputeLayoutsRectXforms(Window window)
        {
            foreach (var layout in _layouts)
                layout.Value.ComputeRectXforms(window);
        }

        /*internal void ComputeTargetRectXforms(string layoutid, string targetid, List<AbstractLayoutElement> content)
        {
            if (!_layouts.ContainsKey(layoutid))
            {
                Locator.Instance.Get<ILogManager>().Error($"No layout with it {layoutid}");
                return;
            }

            if (_layouts[layoutid].GetTarget(targetid) == null)
            {
                Locator.Instance.Get<ILogManager>().Error($"Layout \"{layoutid}\" does not contain target with id {targetid}");
                return;
            }

            var target = _layouts[layoutid].GetTarget(targetid) as AbstractLayoutElementComposite;
            if (target == null)
            {
                Locator.Instance.Get<ILogManager>().Error($"Target \"{targetid}\" cannot contain childrens");
                return;
            }

            target.ComputeContentSizesAndPositions(content);
        }*/
    }
}
