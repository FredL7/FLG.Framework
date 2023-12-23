using FLG.Cs.Logger;
using FLG.Cs.Serialization;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI;

namespace FLG.Cs.Factory {
    public static class ManagerFactory {
        public static void CreateLogger(string logsDir)
        {
            ILogManager logManager = new LogManager(logsDir);
            Locator.Instance.Register(logManager);
        }

        public static void CreateSerializer(ESerializerType t, string saveDir)
        {
            ISerializerManager serializer = new SerializerManager(t, saveDir);
            Locator.Instance.Register(serializer);
        }

        public static void CreateUIManager(string layoutsDir, string pagesDir)
        {
            // TODO: Use params
            IUIManager manager = new UIManager();
            Locator.Instance.Register(manager);
        }
    }
}
