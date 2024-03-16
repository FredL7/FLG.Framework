using FLG.Cs.IDatamodel;
using FLG.Cs.Logger;
using FLG.Cs.Math;
using FLG.Cs.Serialization;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI;


namespace FLG.Cs.Framework {
    internal static class ManagersFactory {
        internal static void CreateGeneric(IServiceInstance service) {
            Locator.Instance.Register(service);
        }

        internal static void CreateProxies()
        {
            ILogManager logManager = new LogManagerProxy();
            Locator.Instance.Register(logManager);

            ISerializerManager serializer = new SerializerManagerProxy();
            Locator.Instance.Register(serializer);

            IUIFactory uIFactory = new UIFactoryProxy();
            Locator.Instance.Register(uIFactory);
            IUIManager manager = new UIManagerProxy();
            Locator.Instance.Register(manager);
        }

        internal static void CreateLogger(PreferencesLogs prefs)
        {
            ILogManager logManager = new LogManager(prefs);
            Locator.Instance.Register(logManager);
        }

        internal static void CreateSerializer(PreferencesSerialization prefs)
        {
            ISerializerManager serializer = new SerializerManager(prefs);
            Locator.Instance.Register(serializer);
        }

        internal static void CreateUIManager(PreferencesUI prefs)
        {
            IUIFactory uiFactory = new UIFactory();
            Locator.Instance.Register(uiFactory);

            IUIManager manager = new UIManager(prefs);
            Locator.Instance.Register(manager);
        }
    }
}
