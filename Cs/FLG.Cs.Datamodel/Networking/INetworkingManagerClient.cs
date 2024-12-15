using FLG.Cs.Datamodel.Commands;


namespace FLG.Cs.Datamodel.Networking {
    public interface INetworkingManagerClient : INetworkingManager {
        public void Initialize(string ip, int port);
        public void SendCommand(ICommand command);
    }
}
