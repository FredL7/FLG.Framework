namespace FLG.Cs.Datamodel {
    public interface INetworkingManager : IServiceInstance, IGameLoopObject {
        //public IPAddress[] GetIPv4Addresses();

        public void InitializeClient(string ip, int port);
        public void InitializeServer(int maxPlayers, int port);
        //public void SendMessage(string message);
    }
}
