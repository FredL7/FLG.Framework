using System.Net;

using FLG.Cs.Datamodel;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    public class NetworkingManager : INetworkingManager {
        private TCPClient _client;
        private TCPServer _server;

        private int _clientIndex = 0, _serverIndex = 0;

        public NetworkingManager(/* PreferencesTCP */)
        {

        }

        #region IServiceInstance
        public bool IsProxy() => false; // TODO: Remove completely
        public void OnServiceRegisteredFail() { Locator.Instance.Get<ILogManager>().Error("Networking Manager Failed to register"); } // TODO: Log directly in locator since I don't do anything than log in these methods
        public void OnServiceRegistered()
        {
            Locator.Instance.Get<ILogManager>().Debug("Serialization Manager Registered");
        }
        #endregion IServiceInstance

        public void Start()
        {
            var host = Dns.GetHostName();

            _server = new();
            _client = new();

            _server.Connect("127.0.0.1", 8052);
            _client.Connect(host, "127.0.0.1", 8052);
        }
    }
}
