using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Layouts;

namespace FLG.Cs.UI {
    public class UIManager : IUIManager {
        private LayoutsManager _layoutsManager;

        public UIManager(string layoutsDir)
        {
            _layoutsManager = new LayoutsManager(layoutsDir);
        }

        #region IServiceInstance
        public bool IsProxy() => false;
        public void OnServiceRegisteredFail() { Locator.Instance.Get<ILogManager>().Error("UI Manager Failed to register"); }
        public void OnServiceRegistered() {
            Locator.Instance.Get<ILogManager>().Debug("UI Manager Registered");
            RegisterLayouts();
        }
        #endregion IServiceInstance

        public IEnumerable<ILayout> GetLayouts()
        {
            return _layoutsManager.GetLayouts();
        }

        private void RegisterLayouts()
        {
            Window defaultWindow = new(1920, 1080);
            // TODO: Get actual window size (may via prefs or via watcher below vvvv)
            // TODO: Register window size change to compute on change (also applies to pages)
            _layoutsManager.RegisterLayouts(defaultWindow);
        }
    }
}
