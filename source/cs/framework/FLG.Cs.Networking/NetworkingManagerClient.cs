using FLG.Cs.Datamodel.Commands;
using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Networking;

using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    public class NetworkingManagerClient : NetworkingManager, INetworkingManagerClient {
        private readonly Client.Client _client;

        public NetworkingManagerClient(PreferencesNetworking prefs) : base(prefs)
        {
            _client = new(this);
        }

        public override void OnServiceRegistered()
        {
            Locator.Instance.Get<ILogManager>().Debug("Networking Manager (Client) Registered");
        }

        public override void SendCommand(ICommand command)
        {
            _client.SendCommand(command);
        }

        public void Connect(string ip, int port)
        {
            _client.ConnectToServer(ip, port);
        }

        public void Disconnect()
        {
            _client.DisconnectFromServer();
        }

        protected override bool GetConnectionStatus() => _client.IsConnected;
    }
}
