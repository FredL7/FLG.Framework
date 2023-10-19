using FLG.Cs.UI.Layouts;

namespace FLG.Cs.UI {
    public interface IUIManager {
        #region Layout
        public void RegisterLayouts(string layoutsDir);
        public IEnumerable<Layout> GetLayouts();
        #endregion Layout

        #region Page
        public void RegisterPages(string pagesDir);
        public void OpenPage(uint id);
        #endregion Page
    }
}
