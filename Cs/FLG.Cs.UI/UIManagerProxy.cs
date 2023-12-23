using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;
using FLG.Cs.ServiceLocator;
using FLG.Cs.Logger;

namespace FLG.Cs.UI {
    public class UIManagerProxy : IUIManager {
        public bool IsProxy() => true;
        public void OnServiceRegistered() { Locator.Instance.Get<ILogManager>().Debug("UI Manager Proxy Registered"); }
        public void OnServiceRegisteredFail() { Locator.Instance.Get<ILogManager>().Error("UI Manager Proxy Failed to register"); }

        // TODO: Don't return null
        public IEnumerable<ILayout> GetLayouts() { return default; }
        public IEnumerable<IPage> GetPages() { return default; }
        public void OpenPage(string id) { }
        public void RegisterLayouts() { }
        public void RegisterPages() { }
    }
}
