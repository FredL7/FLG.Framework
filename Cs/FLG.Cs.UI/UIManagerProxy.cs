using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;
using FLG.Cs.ServiceLocator;
using FLG.Cs.Logger;

namespace FLG.Cs.UI {
    public class UIManagerProxy : IUIManager {
        public bool IsProxy() => true;
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered() { }

        public IEnumerable<ILayout> GetLayouts() { return new List<ILayout>(); }
        public IEnumerable<IPage> GetPages() { return new List<IPage>(); }
        public void OpenPage(string id) { }
    }
}
