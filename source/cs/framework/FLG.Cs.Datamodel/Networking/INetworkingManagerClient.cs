namespace FLG.Cs.Datamodel.Networking {
    public interface INetworkingManagerClient : INetworkingManager {
        public void Connect(string ip, int port);
        public void Disconnect();
    }
}
