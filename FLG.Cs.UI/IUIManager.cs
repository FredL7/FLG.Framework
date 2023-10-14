using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;

namespace FLG.Cs.UI {
    public interface IUIManager {
        #region Layout
        public void RegisterLayout(uint id, AbstractLayout layout);
        public void ComputeLayoutsRectXforms(float width, float height);
        #endregion Layout

        #region Page
        public void RegisterPage(uint id, AbstractPage page);
        public void OpenPage(uint id);
        #endregion Page
    }
}
