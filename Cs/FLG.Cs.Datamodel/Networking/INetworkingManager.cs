namespace FLG.Cs.Datamodel {
    public interface INetworkingManager : IServiceInstance, IGameLoopObject {
        public void SetMaxServerConnexions(int maxConnexions);
        public void InitializeClient(string ip, int port);
        public void InitializeServer(int port);

        public void SendCommand(ICommand command);
    }
}
