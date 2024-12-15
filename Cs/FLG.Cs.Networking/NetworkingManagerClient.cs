using FLG.Cs.Datamodel.Commands;
using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Networking;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    public class NetworkingManagerClient(PreferencesNetworking prefs) : NetworkingManager(prefs), INetworkingManagerClient {
        private Client? _client;

        public int Id {
            get {
                if (_client == null)
                    throw new InvalidOperationException("Client not initialized");
                return _client.Id;
            }
        }

        public string LogIdentifier {
            get {
                if (_client == null)
                {
                    return "Unknown Client";
                }
                else
                {
                    return $"Client {Id}";
                }
            }
        }

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
