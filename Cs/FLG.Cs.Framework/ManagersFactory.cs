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
                case ENetworkClientType.SERVER:
                    INetworkingManagerServer server = new NetworkingManagerServer(prefs);
                    if (!Locator.Instance.Register(server))
                    {
                        result.result = new Result("Could not register Networking Manager (Server)");
                        result.manager = null;
                        return result;
                    }
                    manager = server;
                    break;
                case ENetworkClientType.CLIENT:
                    INetworkingManagerClient client = new NetworkingManagerClient(prefs);
                    if (!Locator.Instance.Register(client))
                    {
                        result.result = new Result("Could not register Networking Manager (Client)");
                        result.manager = null;
                        return result;
                    }
                    manager = client;
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
