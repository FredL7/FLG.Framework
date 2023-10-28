using FLG.Cs.ServiceLocator;
using FLG.Cs.UI;

namespace FLG.Cs.Factory {
    public static class ManagerFactory {
        public static void IncludeUIManager()
        {
            IUIManager manager = new UIManager();
            SingletonManager.Instance.Register(manager);
        }
    }
}
