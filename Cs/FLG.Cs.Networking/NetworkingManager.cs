using System.Net;

using FLG.Cs.Datamodel;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    public class NetworkingManager : INetworkingManager {
        private TCPClient? _client;
        private TCPServer? _server;

        bool initializedClient = false, initializedServer = false;

        // private int _clientIndex = 0, _serverIndex = 0;

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

        public void InitializeClient(string ip, int port)
        {
            if (initializedClient)
            {
                var logger = Locator.Instance.Get<ILogManager>();
                logger.Warn("Cannot initialized Client: Client already initialized");
                return;
            }

            /*if (initializedServer)
            {
                var logger = Locator.Instance.Get<ILogManager>();
                logger.Warn("Cannot initialized Client: Already initialized as Server");
                return;
            }*/

            initializedClient = true;
            _client = new();
            var host = Dns.GetHostName();
            _client.Connect(host, ip, port);
        }

        public void InitializeServer(string ip, int port)
        {
            if (initializedServer)
            {
                var logger = Locator.Instance.Get<ILogManager>();
                logger.Warn("Cannot initialized Server: Server already initialized");
                return;
            }

            /*if (initializedClient)
            {
                var logger = Locator.Instance.Get<ILogManager>();
                logger.Warn("Cannot initialized Server: Already initialized as Client");
                return;
            }*/

            initializedServer = true;
            _server = new();
            _server.Connect(ip, port);
        }

        public void SendMessage(string message)
        {
            if (initializedClient && _client != null)
            {
                _client.SendMessage(message);
            }
            else if (initializedServer && _server != null)
            {
                _server?.SendMessage(message);
            }
            else
            {
                var logger = Locator.Instance.Get<ILogManager>();
                logger.Warn("Client or Server not initialized");
            }
        }

        public void TmpSendMessageClient(string message)
        {
            if (initializedClient && _client != null)
            {
                _client.SendMessage(message);
            }
        }

        public void TmpSendMessageServer(string message)
        {
            if (initializedServer && _server != null)
            {
                _server?.SendMessage(message);
            }
        }
    }
}
