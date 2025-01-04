namespace FLG.Cs.Datamodel.Networking {
    public interface INetworkingManagerServer : INetworkingManager {
        public void Start(int port);
        public void Stop();
    }
}
