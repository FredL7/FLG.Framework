namespace FLG.Cs.Datamodel {
    public interface INetworkingManagerClient : INetworkingManager {
        public void Initialize(string ip, int port);
        public void SendCommand(ICommand command);
    }
}
