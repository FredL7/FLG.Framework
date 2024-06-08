using FLG.Cs.Datamodel;
using FLG.Cs.Logger;
using FLG.Cs.Networking;
using FLG.Cs.Serialization;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI;


namespace FLG.Cs.Framework {
    internal static class ManagersFactory {
        internal static void CreateGeneric<T>(T service) where T : IServiceInstance
        {
            Locator.Instance.Register(service);
        }

        internal static void CreateLogger(PreferencesLogs prefs, bool dummy)
        {
            ILogManager logManager = dummy ? new LogManagerDummy() : new LogManager(prefs);
            Locator.Instance.Register(logManager);
        }

        internal static void CreateSerializer(PreferencesSerialization prefs)
        {
            ISerializerManager serializer = new SerializerManager(prefs);
            Locator.Instance.Register(serializer);
        }

        internal static void CreateUIManager(PreferencesUI prefs)
        {
            IUIManager manager = new UIManager(prefs);
            Locator.Instance.Register(manager);
        }

        internal static void CreateNetworkingManager(PreferencesNetworking prefs)
        {
            INetworkingManager manager = new NetworkingManager(prefs);
            Locator.Instance.Register(manager);
        }
    }
}
