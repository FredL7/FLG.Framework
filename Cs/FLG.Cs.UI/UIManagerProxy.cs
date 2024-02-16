using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;
using FLG.Cs.UI.Commons;

namespace FLG.Cs.UI {
    public class UIManagerProxy : IUIManager {
        public bool IsProxy() => true;
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered() { }

        public IEnumerable<ILayout> GetLayouts() { return new List<ILayout>(); }
        public void RegisterPage(IPage p) { }
    }
}
