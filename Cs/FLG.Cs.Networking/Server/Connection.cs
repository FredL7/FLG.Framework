using System.Net.Sockets;

using FLG.Cs.Datamodel.Logger;

using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking.Server {
    internal class Connection : IConnection {
        private readonly TCPServer _tcp;
        private readonly Server _server;

        public int ID { get; private set; }
        public IProtocol Tcp { get => _tcp; }
        public string ConnectionType { get; set; }

        public Connection(int id, Server server, ThreadManager threadManager)
        {
            ID = id;
            ConnectionType = "UNKNOWN";
            _server = server;
            _tcp = new TCPServer(this, _server.Receiver, threadManager);
        }

        public void Connect(TcpClient tcpclient)
        {
            _tcp.Connect(tcpclient, () => _server.Sender.Welcome(ID));
        }

        public void Disconnect()
        {
            Locator.Instance.Get<ILogManager>().Debug($"Connection {ID} ({_tcp.IP}) has disconnected");
            _tcp.Disconnect();
            _server.Sender.Disconnected(ID);

            _server.OnConnectionDisconnected(ID);
        }
    }
}
