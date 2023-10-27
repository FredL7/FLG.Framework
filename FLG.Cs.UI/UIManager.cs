using FLG.Cs.Logger;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;

namespace FLG.Cs.UI {
    public class UIManager : IUIManager {
        private LayoutsManager _layoutsManager;
        private PagesManager _pagesManager;

        public UIManager()
        {
            _layoutsManager = new LayoutsManager();
            _pagesManager = new PagesManager();
        }

        #region Layouts
        public void RegisterLayouts(string layoutsDir)
        {
            Window defaultWindow = new(1920, 1080);
            // TODO: Register window size change to compute on change (also applies to pages)
            _layoutsManager.RegisterLayouts(layoutsDir, defaultWindow);
        }

        public IEnumerable<ILayout> GetLayouts()
        {
            return _layoutsManager.GetLayouts();
        }
        #endregion Layouts

        #region Page
        public void RegisterPages(string pagesDir)
        {
            _pagesManager.RegisterPages(pagesDir);
            foreach (var page in _pagesManager.GetPages())
                foreach (var target in page.GetTargetsId())
                {
                    _layoutsManager.ComputeTargetRectXforms(page.LayoutId, target, page.GetContentElements(target));
                    LogManager.Instance.Info($"Page \"{page.GetName()}\": registered content for target \"{target}\" in layout \"{page.LayoutId}\"");
                }
        }

        public void OpenPage(string pageId)
        {
            var layoutId = _pagesManager.GetLayoutIdFromPageId(pageId);
            _layoutsManager.SetLayoutActive(layoutId);
            _pagesManager.OpenPage(pageId);
        }

        public IEnumerable<IPage> GetPages()
        {
            return _pagesManager.GetPages();
        }
        #endregion Page
    }
}
