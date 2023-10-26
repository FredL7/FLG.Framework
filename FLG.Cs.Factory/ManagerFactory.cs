using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI;

namespace FLG.Cs.Factory {
    public static class ManagerFactory {
        public static void IncludeLogger(string logDir)
        {
            LogManager.Instance.SetLogLocation(logDir);
        }

        public static void IncludeUIManager()
        {
            IUIManager manager = new UIManager();
            SingletonManager.Instance.Register(manager);
        }
    }
}
