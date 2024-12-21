using FLG.Cs.Datamodel.Commands;
using FLG.Cs.Datamodel.Networking;


// Resource: https://www.youtube.com/watch?v=4uHTSknGJaY&list=PLXkn83W0QkfnqsK8I0RAz5AbUxfg3bOQ5
namespace FLG.Cs.Networking {
    public abstract class NetworkingManager : INetworkingManager {
        private readonly ThreadManager _threadManager;
        internal ThreadManager ThreadManager { get => _threadManager; }

        public NetworkingManager(PreferencesNetworking prefs)
        {
            _threadManager = new();
        }

        public abstract void OnServiceRegistered();
        public abstract void SendCommand(ICommand command);

        public void Update()
        {
            _threadManager.Update();
        }
    }
}
