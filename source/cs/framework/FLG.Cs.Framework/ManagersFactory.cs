using FLG.Cs.Datamodel.Commands;
using FLG.Cs.Datamodel.Framework;
using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Networking;
using FLG.Cs.Datamodel.Serialization;
using FLG.Cs.Datamodel.UI;
using FLG.Cs.Datamodel.Validation;

using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;
using FLG.Cs.Serialization;
using FLG.Cs.UI;
using FLG.Cs.Networking;
using FLG.Cs.Commands;


namespace FLG.Cs.Framework {
    internal static class ManagersFactory {
        internal static FrameworkFactoryResult<ILogManager> CreateLogger(PreferencesLogs prefs)
        {
            FrameworkFactoryResult<ILogManager> result;
            LogManager manager = new(prefs);

            ILogManager imanager = manager;
            if (Locator.Instance.Register(imanager))
            {
                result.result = Result.SUCCESS;
                result.manager = imanager;
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
            var logger = Locator.Instance.Get<ILogManager>();
            var factory = new UIFactory();

            FrameworkFactoryResult<IUIManager> result;
            IUIManager manager = new UIManager(prefs, logger, factory);
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
                case ENetworkingClientType.SERVER:
                    INetworkingManagerServer server = new NetworkingManagerServer(prefs);
                    if (!Locator.Instance.Register(server))
                    {
                        result.result = new Result("Could not register Networking Manager (Server)");
                        result.manager = null;
                        return result;
                    }
                    manager = (NetworkingManager)server; // For correct log message when registering
                    break;
                case ENetworkingClientType.CLIENT:
                    INetworkingManagerClient client = new NetworkingManagerClient(prefs);
                    if (!Locator.Instance.Register(client))
                    {
                        result.result = new Result("Could not register Networking Manager (Client)");
                        result.manager = null;
                        return result;
                    }
                    manager = (NetworkingManager)client;
                    break;
                default:
                    result.result = new Result($"Unknown network client type {prefs.clientType}");
                    result.manager = null;
                    return result;
            }

            // Register as INetworkingManager as well
            // TODO: Figure out logging (does't log as NetworkingManager, but as Server or Client, even with cast)
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
