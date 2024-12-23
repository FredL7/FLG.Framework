using FLG.Cs.Datamodel.Commands;
using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Networking;

using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    public class NetworkingManagerServer : NetworkingManager, INetworkingManagerServer {
        private readonly Server.Server _server;

        public NetworkingManagerServer(PreferencesNetworking prefs) : base(prefs)
        {
            _server = new(ThreadManager);
        }

        public override void OnServiceRegistered()
        {
            Locator.Instance.Get<ILogManager>().Debug("Networking Manager (Server) Registered");
        }

        public override void SendCommand(ICommand command)
        {
            _server.SendCommand(command);
        }

        public void Start(int port)
        {
            _server.Start(port);
        }

        public void Stop()
        {
            _server.Stop();
        }
    }
}
