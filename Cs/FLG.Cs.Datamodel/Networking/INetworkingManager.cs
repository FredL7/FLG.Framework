using FLG.Cs.Datamodel.Framework;
using FLG.Cs.Datamodel.ServiceLocator;

namespace FLG.Cs.Datamodel.Networking {
    public interface INetworkingManager : IServiceInstance, IGameLoopObject {
        public int Id { get; }
        public string LogIdentifier { get; }
    }
}
