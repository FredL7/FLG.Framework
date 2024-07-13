using FLG.Cs.Commands;
using FLG.Cs.Datamodel;
using FLG.Cs.Logger;
using FLG.Cs.Model;
using FLG.Cs.Networking;
using FLG.Cs.Serialization;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI;


namespace FLG.Cs.Framework {
    internal static class ManagersFactory {
        internal static FrameworkFactoryResult<ILogManager> CreateLogger(PreferencesLogs prefs, bool dummy)
        {
            FrameworkFactoryResult<ILogManager> result;
            ILogManager manager = dummy ? new LogManagerDummy() : new LogManager(prefs);
            if (Locator.Instance.Register(manager))
            {
                result.result = Result.SUCCESS;
                result.manager = manager;
            }
            else
            {
                result.result = new Result("Could not register Log Manager");
                result.manager = null;
            }

            return result;
        }

        internal static FrameworkFactoryResult<ISerializerManager> CreateSerializer(PreferencesSerialization prefs)
        {
            FrameworkFactoryResult<ISerializerManager> result;
            ISerializerManager manager = new SerializerManager(prefs);
            if (Locator.Instance.Register(manager))
            {
                result.result = Result.SUCCESS;
                result.manager = manager;
            }
            else
            {
                result.result = new Result("Could not register Serializer Manager");
                result.manager = null;
            }

            return result;
        }

        internal static FrameworkFactoryResult<IUIManager> CreateUIManager(PreferencesUI prefs)
        {
            FrameworkFactoryResult<IUIManager> result;
            IUIManager manager = new UIManager(prefs);
            if (Locator.Instance.Register(manager))
            {
                result.result = Result.SUCCESS;
                result.manager = manager;
            }
            else
            {
                result.result = new Result("Could not register UI Manager");
                result.manager = null;
            }

            return result;
        }

        internal static FrameworkFactoryResult<INetworkingManager> CreateNetworkingManager(PreferencesNetworking prefs)
        {
            FrameworkFactoryResult<INetworkingManager> result;
            INetworkingManager manager;
            switch (prefs.clientType)
            {
                case ENetworkClientType.SERVER:
                    manager = new NetworkingManagerServer(prefs);
                    break;
                case ENetworkClientType.CLIENT:
                    manager = new NetworkingManagerClient(prefs);
                    break;
                default:
                    result.result = new Result($"Unknown network client type {prefs.clientType}");
                    result.manager = null;
                    return result;
            }

            if (Locator.Instance.Register(manager))
            {
                result.result = Result.SUCCESS;
                result.manager = manager;
            }
            else
            {
                result.result = new Result("Could not register Networking Manager");
                result.manager = null;
            }

            return result;
        }

        internal static FrameworkFactoryResult<ICommandManager> CreateCommandManager()
        {
            FrameworkFactoryResult<ICommandManager> result;
            ICommandManager manager = new CommandManager();
            if (Locator.Instance.Register(manager))
            {
                result.result = Result.SUCCESS;
                result.manager = manager;
            }
            else
            {
                result.result = new Result("Could not register Command Manager");
                result.manager = null;
            }

            return result;
        }
    }
}
