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
            ILogManager manager;
            switch(prefs.loggerType)
            {
                case ELoggerType.WRITE_FILE:
                    manager = new LogManagerWriteFile(prefs);
                    break;
                case ELoggerType.NO_LOGS:
                    manager = new LogManagerNoLogs(prefs);
                    break;
                case ELoggerType.NETWORKING:
                    manager = new LogManagerNetworking(prefs);
                    break;
                case ELoggerType.GAME_ENGINE:
                    result.result = new Result($"Can't create a Game Engine Logger");
                    result.manager = null;
                    return result;
                default:
                    result.result = new Result($"Unknown Logger type {prefs.loggerType}");
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
