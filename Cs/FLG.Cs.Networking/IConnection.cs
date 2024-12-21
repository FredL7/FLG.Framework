namespace FLG.Cs.Networking {
    internal interface IConnection {
        public int ID { get; }
        public void Disconnect();
    }
}
