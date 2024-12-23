using FLG.Cs.Datamodel.Logger;

using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking.Client {
    internal class Connection : IConnection {
        private readonly TCPClient _tcp;
        private readonly Client _client;

        private bool _isConnected = false;

        public int ID { get; set; }
        public IProtocol Tcp { get => _tcp; }

        public Connection(Client client, ThreadManager threadManager)
        {
            _client = client;
            _tcp = new TCPClient(this, _client.Receiver, threadManager);
        }

        public void Connect(string ip, int port)
        {
            _tcp.Connect(ip, port);
            _isConnected = true;
        }

        public void Disconnect()
        {
            if (_isConnected)
            {
                _isConnected = false;
                _tcp.Disconnect();

                Locator.Instance.Get<ILogManager>().Debug("Disconnected from server");
            }
        }
    }
}
