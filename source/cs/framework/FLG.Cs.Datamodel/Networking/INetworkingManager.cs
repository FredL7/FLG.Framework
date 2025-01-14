using FLG.Cs.Datamodel.Commands;
using FLG.Cs.Datamodel.Framework;
using FLG.Cs.Datamodel.ServiceLocator;


namespace FLG.Cs.Datamodel.Networking {
    public interface INetworkingManager : IServiceInstance, IGameLoopObject {
        public bool IsConnected { get; }
        public void SendCommand(ICommand command);
    }
}
