namespace FLG.Cs.Datamodel {
    public interface INetworkingManager : IServiceInstance, IGameLoopObject {
        //public IPAddress[] GetIPv4Addresses();

        public void SetMaxServerConnexions(int maxConnexions);
        public void InitializeClient(string ip, int port);
        public void InitializeServer(int port);

        public void SendCommand(ICommand command);
        //public void SendMessage(string message);
    }
}
