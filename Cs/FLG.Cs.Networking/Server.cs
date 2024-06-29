using System.Net;
using System.Net.Sockets;

using FLG.Cs.Datamodel;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    internal class Server {
        private readonly NetworkingManager _manager;

        private readonly int _port;
        private readonly TcpListener _tcpListener;

        private bool _maxConnexionsSetOnlyOnce = false;
        private int _maxConnexions = 0;

        protected Dictionary<int, TCPConnexion> _connexions;
        internal delegate void MessageHandler(int clientID, Message message);
        private readonly Dictionary<int, MessageHandler> _messageHandlers;
        internal Dictionary<int, MessageHandler> MessageHandlers { get => _messageHandlers; }

        public Server(int port, NetworkingManager manager)
        {
            var logger = Locator.Instance.Get<ILogManager>();

            _manager = manager;
            _port = port;
            _connexions = new();

            _messageHandlers = new()
            {
                { (int)Messages.WELCOME, WelcomeHandler },
                { (int)Messages.HEARTBEAT, HeartbeatHandler },
                { (int)Messages.COMMAND, CommandHandler },
            };

            logger.Debug($"Starting Server");

            _tcpListener = new(IPAddress.Any, _port);
            _tcpListener.Start();
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            logger.Debug($"Server started on {_port}");
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
                    _connexions.Add(i, new TCPConnexion(i, _manager, this));
                }
            }
            Locator.Instance.Get<ILogManager>().Debug($"Server max connexions set to {_maxConnexions}");
        }

        #region Connexion
        private void TCPConnectCallback(IAsyncResult result)
        {
            TcpClient client = _tcpListener.EndAcceptTcpClient(result);
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Locator.Instance.Get<ILogManager>().Debug($"Incoming connxion from {client.Client.RemoteEndPoint}");

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
            message.Write("Welcome to the server");
            message.Write(clientId);

            Locator.Instance.Get<ILogManager>().Debug("Sending Welcome message to client (WELCOME, MESSAGE, CLIENT ID)");
            SendTCPData(clientId, message);
        }
        #endregion Connexion

        #region TCP
        private void SendTCPData(int clientId, Message message)
        {
            message.WriteLength();
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
            Locator.Instance.Get<ILogManager>().Debug("Receiving welcome message from client");
            // int clientIdCheck = message.ReadInt();
            // string username = message.ReadString();

            var logger = Locator.Instance.Get<ILogManager>();

            logger.Debug("Received Welcome message from client (WELCOME, CLIENT ID, USERNAME)");

            logger.Debug($"{_connexions[clientId].RemoteEndPoint} connected successfully and is now player {clientId}");
            /*if (clientId != clientIdCheck)
            {
                logger.Error($"Player \"{username}\" (ID: {clientId}) has assumed the wrong client ID ({clientIdCheck})");
            }*/
        }

        private void HeartbeatHandler(int cliendId, Message message)
        {
            throw new NotImplementedException();
        }

        private void CommandHandler(int cliendId, Message message)
        {
            throw new NotImplementedException();
        }
        #endregion Message Handlers
    }
}
