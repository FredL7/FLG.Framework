using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;

namespace FLG.Cs.UI {
    public interface IUIManager : IServiceInstance {
        #region Layout
        public void RegisterLayouts();
        public IEnumerable<ILayout> GetLayouts();
        #endregion Layout

        #region Page
        public void RegisterPages();
        public void OpenPage(string id);
        public IEnumerable<IPage> GetPages();
        #endregion Page
    }
}
