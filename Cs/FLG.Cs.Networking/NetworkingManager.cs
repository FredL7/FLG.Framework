using FLG.Cs.Datamodel.Commands;
using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Networking;
using FLG.Cs.ServiceLocator;


// Resource: https://www.youtube.com/watch?v=4uHTSknGJaY&list=PLXkn83W0QkfnqsK8I0RAz5AbUxfg3bOQ5
namespace FLG.Cs.Networking {
    public abstract class NetworkingManager : INetworkingManager {
        private readonly ThreadManager _threadManager;
        internal ThreadManager ThreadManager { get => _threadManager; }

        public NetworkingManager(PreferencesNetworking prefs)
        {
            _threadManager = new();
        }

        public virtual void OnServiceRegistered()
        {
            Locator.Instance.Get<ILogManager>().Debug("Networking Manager (interface) Registered");
        }
        public abstract void SendCommand(ICommand command);

        public void Update()
        {
            _threadManager.Update();
        }
    }
}
