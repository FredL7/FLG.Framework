using FLG.Cs.Datamodel;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    public class NetworkingManagerClient : NetworkingManager, INetworkingManagerClient {
        private Client? _client;

        public int Id {
            get {
                if (_client == null)
                    throw new InvalidOperationException("Client not initialized");
                return _client.Id;
            }
        }

        public NetworkingManagerClient(PreferencesNetworking prefs) :base(prefs) { }

        #region IServiceInstance
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered()
        {
            Locator.Instance.Get<ILogManager>().Debug("Client Networking Manager Registered");
        }
        #endregion IServiceInstance

        public void Initialize(string ip, int port)
        {
            if (_client != null)
            {
                Locator.Instance.Get<ILogManager>().Warn("Client Networking Manager already initialized");
                return;
            }

            _client = new Client(ip, port, this);
            _client.ConnectToServer();
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
    }
}
