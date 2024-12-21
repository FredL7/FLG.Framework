namespace FLG.Cs.Networking {
    internal interface IReceiver {
        public void HandlePacket(int packetType, int connectionId, Packet packet);
    }
}
