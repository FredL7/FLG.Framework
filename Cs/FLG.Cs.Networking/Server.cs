using System.Net;
using System.Net.Sockets;

using FLG.Cs.Datamodel;
using FLG.Cs.ServiceLocator;
using static FLG.Cs.Networking.MessagesHandler;


namespace FLG.Cs.Networking {
    internal class Server {
        private readonly NetworkingManager _manager;
        public NetworkingManager Manager { get => _manager; }

        private const int _id = -1;

        private readonly int _port;
        public int Port { get => _port; }
        private readonly TcpListener _tcpListener;

        private bool _maxConnexionsSetOnlyOnce = false;
        private int _maxConnexions = 0;
        public int MaxConnexions { get => _maxConnexions; }

        protected Dictionary<int, TCPConnexion> _connexions;
        private readonly MessagesHandler _messagesHandler;
        public Dictionary<int, MessageHandler> MessageHandlers { get => _messagesHandler.MessageHandlers; }

        public Server(int port, NetworkingManager manager)
        {
            var logger = Locator.Instance.Get<ILogManager>();

            _manager = manager;
            _port = port;
            _connexions = new();

            _messagesHandler = new(new()
            {
                { (int)Messages.WELCOME, WelcomeHandler },
                { (int)Messages.HEARTBEAT, HeartbeatHandler },
                { (int)Messages.COMMAND, CommandHandler },
            });

            logger.Info($"Starting Server");

            _tcpListener = new(IPAddress.Any, _port);
            _tcpListener.Start();
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            logger.Info($"Server started on {_port}");
        }

        public void SetMaxConnexions(int nbConnexions)
        {
            if (!_maxConnexionsSetOnlyOnce)
            {
                _maxConnexionsSetOnlyOnce = true;
                _maxConnexions = nbConnexions;

                _connexions = new(nbConnexions);
                for (int i = 0; i < _maxConnexions; ++i)
                {
                    _connexions.Add(i, new TCPConnexion(i, this));
                }
            }
            Locator.Instance.Get<ILogManager>().Info($"Server max connexions set to {_maxConnexions}");
        }

        #region Connexion
        private void TCPConnectCallback(IAsyncResult result)
        {
            TcpClient client = _tcpListener.EndAcceptTcpClient(result);
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Locator.Instance.Get<ILogManager>().Debug($"Incoming connexion from {client.Client.RemoteEndPoint}");

            for (int i = 0; i < _maxConnexions; ++i)
            {
                if (_connexions[i].Available)
                {
                    _connexions[i].Connect(client);
                    SendWelcomeMessage(_connexions[i].Id);
                    return;
                }
            }
        }

        private void SendWelcomeMessage(int clientId)
        {
            using Message message = new((int)Messages.WELCOME);
            message.Write("Welcome to the server", "welcomemsg");
            message.Write(clientId, "clientid");

            Locator.Instance.Get<ILogManager>().Debug("Sending Welcome message to client");
            SendTCPData(clientId, message);
        }
        #endregion Connexion

        #region TCP
        private void SendTCPData(int clientId, Message message)
        {
            message.WriteId(_id);
            message.WriteLength();
            Locator.Instance.Get<ILogManager>().Debug($"Sending message to client {clientId} ({message})");
            _connexions[clientId].SendData(message);
        }

        private void SendTCPDataToAll(Message message)
        {
            foreach (var client in _connexions)
            {
                client.Value.SendData(message);
            }
        }

        private void SendTCPDataTpAllButOne(int ExceptId, Message message)
        {
            foreach (var client in _connexions)
            {
                if (client.Value.Id != ExceptId)
                {
                    client.Value.SendData(message);
                }
            }
        }
        #endregion TCP

        #region Message Handlers
        private void WelcomeHandler(int clientId, Message message)
        {
            Locator.Instance.Get<ILogManager>().Debug($"Receiving welcome message from client {clientId}");

            var logger = Locator.Instance.Get<ILogManager>();
            logger.Debug($"{_connexions[clientId].RemoteEndPoint} connected successfully and is now player {clientId}");
        }

        private void HeartbeatHandler(int clientId, Message message)
        {
            throw new NotImplementedException();
        }

        private void CommandHandler(int clientId, Message message)
        {
            var logger = Locator.Instance.Get<ILogManager>();
            var cmd = Locator.Instance.Get<ICommandManager>();

            logger.Debug($"Receiving command message from client {clientId}");
            string command = message.ReadString();
            logger.Debug($"Received command from client {clientId} ({command})");

            cmd.ExecuteCommand(command);
        }
        #endregion Message Handlers
    }
}
