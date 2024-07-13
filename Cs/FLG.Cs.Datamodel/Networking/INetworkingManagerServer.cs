namespace FLG.Cs.Datamodel {
    public interface INetworkingManagerServer : INetworkingManager {
        public int MaxConnexions { get; }

        public void Initialize(int port, int maxConnexions);
        public void SendCommandToClient(int clientId, ICommand command);
        public void SendCommandToAll(ICommand command);
        public void SendCommandToAllButOne(int exceptId, ICommand command);
    }
}
