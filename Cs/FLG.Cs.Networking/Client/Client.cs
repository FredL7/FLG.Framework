using FLG.Cs.Datamodel.Commands;


namespace FLG.Cs.Networking.Client {
    internal class Client {
        private readonly Connection _connection;
        internal Connection Connection { get => _connection; }

        internal Receiver Receiver { get; private set; }
        internal Sender Sender { get; private set; }

        public Client(NetworkingManagerClient manager)
        {
            Receiver = new(this);
            Sender = new(this);

            _connection = new Connection(this, manager.ThreadManager);
        }

        public void SendCommand(ICommand command)
        {
            Sender.Command(command);
        }

        public void ConnectToServer(string ip, int port)
        {
            _connection.Connect(ip, port);
        }

        public void DisconnectFromServer()
        {
            _connection?.Disconnect();
        }
    }
}
