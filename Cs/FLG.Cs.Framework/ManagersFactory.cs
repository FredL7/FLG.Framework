using FLG.Cs.IDatamodel;
using FLG.Cs.Logger;
using FLG.Cs.Serialization;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI;


namespace FLG.Cs.Framework {
    internal static class ManagersFactory {
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

        internal static void CreateLogger(string logsDir)
        {
            ILogManager logManager = new LogManager(logsDir);
            Locator.Instance.Register(logManager);
        }

        internal static void CreateSerializer(ESerializerType t, string saveDir)
        {
            ISerializerManager serializer = new SerializerManager(t, saveDir);
            Locator.Instance.Register(serializer);
        }

        internal static void CreateUIManager(string layoutsDir, string pagesDir)
        {
            IUIFactory uiFactory = new UIFactory();
            Locator.Instance.Register(uiFactory);

            IUIManager manager = new UIManager(layoutsDir, pagesDir);
            Locator.Instance.Register(manager);
        }
    }
}
