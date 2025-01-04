using FLG.Cs.Datamodel.Commands;


namespace FLG.Cs.Networking.Server {
    internal class Sender(Server server) {
        private readonly Server _server = server;

        #region TCP
        public void SendTCPData(int cliendId, Packet packet)
        {
            packet.WriteLength();
            _server.GetConnection(cliendId).Tcp.SendData(packet);
        }

        public void SendTCPDataToAll(Packet packet)
        {
            packet.WriteLength();
            foreach (var kvp in _server.GetConnections())
            {
                kvp.Value.Tcp.SendData(packet);
            }
        }

        public void SendTcpDataToAll(int excludeId, Packet packet)
        {
            packet.WriteLength();
            foreach (var kvp in _server.GetConnections())
            {
                if (excludeId != kvp.Key)
                {
                    kvp.Value.Tcp.SendData(packet);
                }
            }
        }
        #endregion TCP

        public void Welcome(int connectionId)
        {
            using Packet packet = new((int)ServerPackets.WELCOME);
            packet.Write(connectionId);
            SendTCPData(connectionId, packet);
        }

        public void Disconnected(int connectionId)
        {
            using Packet packet = new((int)ServerPackets.DISCONNECTED);
            packet.Write(connectionId);
            SendTCPDataToAll(packet);
        }

        public void Command(ICommand command)
        {
            using Packet packet = new((int)ServerPackets.COMMAND);
            string commandstring = command.ToPacketString();
            packet.Write(commandstring);
            SendTCPDataToAll(packet);
        }
    }
}
