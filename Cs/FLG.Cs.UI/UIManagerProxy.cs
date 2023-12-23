using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;
using FLG.Cs.ServiceLocator;
using FLG.Cs.Logger;

namespace FLG.Cs.UI {
    public class UIManagerProxy : IUIManager {
        public bool IsProxy() => true;
        public void OnServiceRegisteredFail() { Locator.Instance.Get<ILogManager>().Error("UI Manager Proxy Failed to register"); }
        public void OnServiceRegistered() { Locator.Instance.Get<ILogManager>().Debug("UI Manager Proxy Registered"); }

        public IEnumerable<ILayout> GetLayouts() { return new List<ILayout>(); }
        public IEnumerable<IPage> GetPages() { return new List<IPage>(); }
        public void OpenPage(string id) { }
    }
}
