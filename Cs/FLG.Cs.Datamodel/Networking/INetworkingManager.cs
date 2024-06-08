namespace FLG.Cs.Datamodel {
    public interface INetworkingManager : IServiceInstance {
        public void InitializeClient(string ip, int port);
        public void InitializeServer(string ip, int port);
        public void SendMessage(string message);

        public void TmpSendMessageClient(string message);
        public void TmpSendMessageServer(string message);
    }
}
