using FLG.Cs.UI.Layouts;

namespace FLG.Cs.UI {
    public interface IUIManager {
        #region Layout
        public void RegisterLayouts(string layoutsDir); //? TODO: string[]
        public IEnumerable<ILayout> GetLayouts();
        #endregion Layout

        #region Page
        public void RegisterPages(string pagesDir);
        public void OpenPage(uint id);
        #endregion Page
    }
}
