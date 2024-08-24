using FLG.Cs.Datamodel;
using FLG.Cs.ServiceLocator;

namespace FLG.Cs.Networking {
    public class NetworkingManagerOffline : INetworkingManager {
        public int Id { get => NetworkingConstants.OFFLINE_ID; }
        public string LogIdentifier { get => "Offline"; }

        public NetworkingManagerOffline(PreferencesNetworking prefs)
        {

        }

        #region IServiceInstance
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered()
        {
            Locator.Instance.Get<ILogManager>().Debug("Offline Networking Manager Registered");
        }
        #endregion IServiceInstance

        public void Update() { }
    }
}
