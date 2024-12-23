using System.Net.Sockets;
using System.Net;

using FLG.Cs.Datamodel.Commands;
using FLG.Cs.Datamodel.Logger;

using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking.Server {
    internal class Server {
        private ThreadManager _threadManager;

        internal Receiver Receiver { get; private set; }
        internal Sender Sender { get; private set; }

        //? : using int for id, where >0 is for client players, and <0 is for utility?
        private readonly Dictionary<int, Connection> _connections;
        internal Connection GetConnection(int id) => _connections[id];
        private TcpListener? _tcpListener;

        public Server(ThreadManager threadManager)
        {
            _threadManager = threadManager;
            _connections = [];
            _tcpListener = null;

            Receiver = new(this);
            Sender = new(this);
        }

        internal IEnumerable<KeyValuePair<int, Connection>> GetConnections()
        {
            foreach (var kvp in _connections)
            {
                yield return kvp;
            }
        }

        public void Start(int port)
        {
            Locator.Instance.Get<ILogManager>().Debug("Starting Server...");

            _tcpListener = new(IPAddress.Any, port);
            _tcpListener.Start();
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Locator.Instance.Get<ILogManager>().Debug($"Server started, listening on port {port}");
        }

        public void Stop()
        {
            // TODO
        }

        public void SendCommand(ICommand command)
        {
            Sender.Command(command);
        }

        private void TCPConnectCallback(IAsyncResult result)
        {
            if (_tcpListener == null)
            {
                Locator.Instance.Get<ILogManager>().Debug("TCP Listener not properly initialized");
                return;
            }

            TcpClient client = _tcpListener.EndAcceptTcpClient(result);
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Locator.Instance.Get<ILogManager>().Debug($"Incoming connection from {client.Client.RemoteEndPoint}...");

            int id = GetNextAvailableId();
            Locator.Instance.Get<ILogManager>().Debug($"Assigning id {id} to {client.Client.RemoteEndPoint}...");
            Connection connection = new(id, this, _threadManager);
            _connections[id] = connection;
            connection.Connect(client); // TODO: return true/false if success?
        }

        private int GetNextAvailableId()
        {
            int nextKey = 0;
            while (_connections.ContainsKey(nextKey))
            {
                ++nextKey;
            }
            return nextKey;
        }

        internal void OnConnectionDisconnected(int id)
        {
            _connections.Remove(id);
        }
    }
}
