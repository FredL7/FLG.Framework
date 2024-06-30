using FLG.Cs.Commands;
using FLG.Cs.Datamodel;
using FLG.Cs.Logger;
using FLG.Cs.Networking;
using FLG.Cs.Serialization;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI;


namespace FLG.Cs.Framework {
    internal static class ManagersFactory {
        internal static ILogManager? CreateLogger(PreferencesLogs prefs, bool dummy)
        {
            ILogManager manager = dummy ? new LogManagerDummy() : new LogManager(prefs);
            if (Locator.Instance.Register(manager))
            {
                return manager;
            }

            return null;
        }

        internal static ISerializerManager? CreateSerializer(PreferencesSerialization prefs)
        {
            ISerializerManager manager = new SerializerManager(prefs);
            if (Locator.Instance.Register(manager))
            {
                return manager;
            }

            return null;
        }

        internal static IUIManager? CreateUIManager(PreferencesUI prefs)
        {
            IUIManager manager = new UIManager(prefs);
            if (Locator.Instance.Register(manager))
            {
                return manager;
            }

            return null;
        }

        internal static INetworkingManager? CreateNetworkingManager(PreferencesNetworking prefs)
        {
            INetworkingManager manager = new NetworkingManager(prefs);
            if (Locator.Instance.Register(manager))
            {
                return manager;
            }

            return null;
        }

        internal static ICommandManager? CreateCommandManager()
        {
            ICommandManager manager = new CommandManager();
            if (Locator.Instance.Register(manager))
            {
                return manager;
            }

            return null;
        }
    }
}
