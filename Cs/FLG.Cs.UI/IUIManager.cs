using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;

namespace FLG.Cs.UI {
    public interface IUIManager : IServiceInstance {
        #region Layout
        public IEnumerable<ILayout> GetLayouts();
        #endregion Layout

        #region Page
        public IEnumerable<IPage> GetPages();
        public void OpenPage(string id);
        #endregion Page
    }
}
