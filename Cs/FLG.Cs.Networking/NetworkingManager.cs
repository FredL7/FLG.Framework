using FLG.Cs.Datamodel;

// Resource: https://www.youtube.com/watch?v=4uHTSknGJaY&list=PLXkn83W0QkfnqsK8I0RAz5AbUxfg3bOQ5


namespace FLG.Cs.Networking {
    public abstract class NetworkingManager {
        private readonly ThreadManager _threadManager;

        public NetworkingManager(PreferencesNetworking pref)
        {
            _threadManager = new();
        }

        internal void ExecuteOnMainThread(Action action)
        {
            _threadManager.ExecuteOnMainThread(action);
        }

        public void Update()
        {
            _threadManager.Update();
        }
    }
}
