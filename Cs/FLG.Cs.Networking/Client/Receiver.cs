using FLG.Cs.Datamodel.Logger;

using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking.Client {
    internal class Receiver : IReceiver {
        private readonly Client _client;

        private delegate void PacketHandler(Packet packet);
        private readonly Dictionary<int, PacketHandler> _packetHandlers;

        public Receiver(Client client)
        {
            _client = client;
            _packetHandlers = new()
            {
                { (int)ServerPackets.WELCOME, Welcome },
                { (int)ServerPackets.DISCONNECTED, Disconnected },
                { (int)ServerPackets.COMMAND, Command },
            };
        }

        public void HandlePacket(int packetId, int _, Packet packet)
        {
            _packetHandlers[packetId](packet);
        }

        private void Welcome(Packet packet)
        {
            int id = packet.ReadInt();
            _client.Connection.ID = id;

            Locator.Instance.Get<ILogManager>().Debug($"Connected to server with id {id}");
        }

        private void Disconnected(Packet packet)
        {
            int id = packet.ReadInt();
            Locator.Instance.Get<ILogManager>().Debug($"Notified of disconnected client with id {id}");
        }

        private void Command(Packet packet)
        {
            string command = packet.ReadString();
            Locator.Instance.Get<ILogManager>().Debug($"Command received from server: {command}");
        }
    }
}
