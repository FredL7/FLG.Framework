using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;

namespace FLG.Cs.UI {
    public class UIManager : IUIManager {
        private LayoutsManager _layoutsManager;
        private PagesManager _pagesManager;

        public UIManager(string layoutsDir, string pagesDir)
        {
            _layoutsManager = new LayoutsManager(layoutsDir);
            _pagesManager = new PagesManager(pagesDir);
        }

        #region IServiceInstance
        public bool IsProxy() => false;
        public void OnServiceRegisteredFail() { Locator.Instance.Get<ILogManager>().Error("UI Manager Failed to register"); }
        public void OnServiceRegistered() {
            Locator.Instance.Get<ILogManager>().Debug("UI Manager Registered");
            RegisterLayouts();
            RegisterPages();
        }
        #endregion IServiceInstance

        #region Layouts
        public IEnumerable<ILayout> GetLayouts()
        {
            return _layoutsManager.GetLayouts();
        }

        private void RegisterLayouts()
        {
            Window defaultWindow = new(1920, 1080);
            // TODO: Register window size change to compute on change (also applies to pages)
            _layoutsManager.RegisterLayouts(defaultWindow);
        }
        #endregion Layouts

        #region Page
        public IEnumerable<IPage> GetPages()
        {
            return _pagesManager.GetPages();
        }

        public void OpenPage(string pageId)
        {
            var layoutId = _pagesManager.GetLayoutIdFromPageId(pageId);
            _layoutsManager.SetLayoutActive(layoutId);
            _pagesManager.OpenPage(pageId);
        }

        private void RegisterPages()
        {
            _pagesManager.RegisterPages();
            foreach (var page in _pagesManager.GetPages())
                foreach (var target in page.GetTargetsId())
                {
                    _layoutsManager.ComputeTargetRectXforms(page.LayoutId, target, page.GetContentElements(target));
                    Locator.Instance.Get<ILogManager>().Info($"Page \"{page.GetName()}\": registered content for target \"{target}\" in layout \"{page.LayoutId}\"");
                }
        }
        #endregion Page
    }
}
