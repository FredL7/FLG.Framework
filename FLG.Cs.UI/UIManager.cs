using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;

namespace FLG.Cs.UI {
    public class UIManager : IUIManager {
        private PagesManager _pagesManager;
        private LayoutsManager _layoutsManager;

        public UIManager()
        {
            _pagesManager = new PagesManager();
            _layoutsManager = new LayoutsManager();
        }

        #region Layout
        public void RegisterLayout(uint id, AbstractLayout layout)
        {
            _layoutsManager.RegisterLayout(id, layout);
        }

        public void ComputeLayoutsRectXforms(float windowWidth, float windowHeight)
        {
            Window window = new(windowWidth, windowHeight);
            _layoutsManager.ComputeLayoutsRectXforms(window);
        }
        #endregion Layout

        #region Page
        public void RegisterPage(uint id, AbstractPage page)
        {
            _pagesManager.RegisterPage(id, page);
        }

        public void OpenPage(uint pageId)
        {
            uint layoutId = _pagesManager.GetLayoutIdFromPageId(pageId);
            _layoutsManager.SetLayoutActive(layoutId);
            _pagesManager.OpenPage(pageId);
        }
        #endregion Page
    }
}
