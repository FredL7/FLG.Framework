using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;

namespace FLG.Cs.UI {
    public interface IUIManager : IServiceInstance {
        #region Layout
        public void RegisterLayouts(string layoutsDir); //? TODO: string[]
        public IEnumerable<ILayout> GetLayouts();
        #endregion Layout

        #region Page
        public void RegisterPages(string pagesDir);
        public void OpenPage(string id);
        public IEnumerable<IPage> GetPages();
        #endregion Page
    }
}
