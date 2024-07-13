using FLG.Cs.Datamodel;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    public class NetworkingManagerServer : NetworkingManager, INetworkingManagerServer {
        private Server? _server;

        public int Id {
            get {
                if (_server == null)
                    throw new InvalidOperationException("Server not initialized");
                return _server.Id;
            }
        }
        public int Port {
            get {
                if (_server == null)
                    throw new InvalidOperationException("Server not initialized");
                return _server.Port;
            }
        }

        public int MaxConnexions {
            get {
                if (_server == null)
                    throw new InvalidOperationException("Server not initialized");
                return _server.MaxConnexions;
            }
        }

        public NetworkingManagerServer(PreferencesNetworking prefs) : base(prefs) { }

        #region IServiceInstance
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered()
        {
            Locator.Instance.Get<ILogManager>().Debug("Server Networking Manager Registered");
        }
        #endregion IServiceInstance

        public void Initialize(int port, int maxConnexions)
        {
            if (_server == null)
                throw new InvalidOperationException("Server not initialized");

            _server = new Server(port, this);
            _server.SetMaxConnexions(maxConnexions); // TODO: move setter to Server ctor
        }

        public void SendCommandToClient(int clientId, ICommand command)
        {
            if (_server == null)
                throw new InvalidOperationException("Server not initialized");

            string commandMessage = command.ToMessageString();
            using Message message = new((int)Messages.COMMAND);
            message.Write(commandMessage);
            Locator.Instance.Get<ILogManager>().Debug($"Sending command message to client {clientId} ({commandMessage})");
            _server.SendTCPData(clientId, message);
        }

        public void SendCommandToAll(ICommand command)
        {
            if (_server == null)
                throw new InvalidOperationException("Server not initialized");

            string commandMessage = command.ToMessageString();
            using Message message = new((int)Messages.COMMAND);
            message.Write(commandMessage);
            Locator.Instance.Get<ILogManager>().Debug($"Sending command message to all clients ({commandMessage})");
            _server.SendTCPDataToAll(message);
        }

        public void SendCommandToAllButOne(int exceptId, ICommand command)
        {
            if (_server == null)
                throw new InvalidOperationException("Server not initialized");

            string commandMessage = command.ToMessageString();
            using Message message = new((int)Messages.COMMAND);
            message.Write(commandMessage);
            Locator.Instance.Get<ILogManager>().Debug($"Sending command message to all clients but {exceptId} ({commandMessage})");
            _server.SendTCPDataTpAllButOne(exceptId, message);
        }
    }
}
