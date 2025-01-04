using System.Net;


namespace FLG.Cs.Networking {
    internal interface IProtocol {
        public EndPoint? IP { get; }

        public void SendData(Packet packet);
        public void Disconnect();
    }
}
