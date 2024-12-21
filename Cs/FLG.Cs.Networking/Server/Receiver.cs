using FLG.Cs.Datamodel.Commands;
using FLG.Cs.Datamodel.Logger;

using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking.Server {
    internal class Receiver : IReceiver {
        private readonly Server _server;

        private delegate void PacketHandler(int connectionId, Packet packet);
        private readonly Dictionary<int, PacketHandler> _packetHandlers;

        public Receiver(Server server)
        {
            _server = server;
            _packetHandlers = new()
            {
                { (int)ClientPackets.WELCOME_RECEIVED, WelcomeReceived },
                { (int)ClientPackets.COMMAND, Command },
            };
        }

        public void HandlePacket(int packetId, int connectionId, Packet packet)
        {
            _packetHandlers[packetId](connectionId, packet);
        }

        private void WelcomeReceived(int connectionId, Packet packet)
        {
            int connectionIdCheck = packet.ReadInt();
            string connectionType = packet.ReadString();

            _server.GetConnection(connectionIdCheck).ConnectionType = connectionType;

            Locator.Instance.Get<ILogManager>().Debug($"{_server.GetConnection(connectionIdCheck).Tcp.IP} connected succesfully as {connectionType} and is now connection {connectionId}");
            if (connectionId != connectionIdCheck)
            {
                Locator.Instance.Get<ILogManager>().Debug($"Connection with ID {connectionId} has assumed the wrong id ({connectionIdCheck})");
            }

            // TODO: callback for connection successfully initialized?
        }

        private void Command(int connectionId, Packet packet)
        {
            var cmd = Locator.Instance.Get<ICommandManager>();

            string command = packet.ReadString();
            Locator.Instance.Get<ILogManager>().Debug($"Received command from client {connectionId}: {command}");

            cmd.ExecuteCommand(command);
        }
    }
}
