using FLG.Cs.Datamodel;
using FLG.Cs.ServiceLocator;

// Resource: https://www.youtube.com/watch?v=4uHTSknGJaY&list=PLXkn83W0QkfnqsK8I0RAz5AbUxfg3bOQ5


namespace FLG.Cs.Networking {
    public class NetworkingManager : INetworkingManager {
        private Client? _client;
        private Server? _server;

        public int ServerPort {
            get {
                if (_server != null)
                {
                    return _server.Port;
                }
                return -1;
            }
        }

        public int MaxServerConnexions { get => _server?.MaxConnexions ?? 0; }
        private readonly ThreadManager _threadManager;

        public NetworkingManager(PreferencesNetworking pref)
        {
            _threadManager = new();
        }

        #region IServiceInstance
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered()
        {
            Locator.Instance.Get<ILogManager>().Debug("Networking Manager Registered");
        }
        #endregion IServiceInstance

        public void InitializeClient(string ip, int port)
        {
            if (_client != null)
            {
                Locator.Instance.Get<ILogManager>().Warn("Client already initialized");
                return;
            }

            if (_server != null)
            {
                Locator.Instance.Get<ILogManager>().Warn("Server already initialized, cannot be both a server and a client");
                return;
            }

            _client = new Client(ip, port, this);
            _client.ConnectToServer();
        }

        public void InitializeServer(int port, int maxConnexions)
        {
            if (_server != null)
            {
                Locator.Instance.Get<ILogManager>().Warn("Server already initialized");
                return;
            }

            if (_client != null)
            {
                Locator.Instance.Get<ILogManager>().Warn("Server already initialized, cannot be both a server and a client");
                return;
            }

            _server = new Server(port, this);
            _server.SetMaxConnexions(maxConnexions);
        }

        public void SendCommand(ICommand command)
        {
            if (_client == null)
            {
                Locator.Instance.Get<ILogManager>().Warn($"Cannot send command, client not initialized ({command.ToMessageString()})");
                return;
            }

            _client.SendCommand(command);
        }

        internal void ExecuteOnMainThread(Action action)
        {
            _threadManager.ExecuteOnMainThread(action);
        }

        public void Update()
        {
            _threadManager.Update();
        }
    }
}
