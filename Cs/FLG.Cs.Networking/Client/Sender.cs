using FLG.Cs.Datamodel.Commands;


namespace FLG.Cs.Networking.Client {
    internal class Sender(Client client) {
        private readonly Client _client = client;

        #region TCP
        private void SendTCPData(Packet packet)
        {
            packet.WriteLength();
            _client.Connection.Tcp.SendData(packet);
        }
        #endregion TCP

        public void WelcomeReceived()
        {
            using Packet packet = new((int)ClientPackets.WELCOME_RECEIVED);
            packet.Write(_client.Connection.ID);
            packet.Write("Player");
            SendTCPData(packet);
        }

        public void Command(ICommand command)
        {
            using Packet packet = new((int)ClientPackets.COMMAND);
            string commandstring = command.ToPacketString();
            packet.Write(commandstring);
            SendTCPData(packet);
        }
    }
}
