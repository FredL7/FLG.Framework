using System.Net.Sockets;


namespace FLG.Cs.Networking.Server {
    internal class TCPServer(IConnection connection, IReceiver receiver, ThreadManager threadManager) : TCP(connection, receiver, threadManager) {
        public void Connect(TcpClient socket, Action callback)
        {
            _socket = socket;
            _socket.ReceiveBufferSize = Constants.DATA_BUFFER_SIZE;
            _socket.SendBufferSize = Constants.DATA_BUFFER_SIZE;

            _stream = socket.GetStream();

            _stream.BeginRead(_receiveBuffer, 0, Constants.DATA_BUFFER_SIZE, ReceiveCallback, null);
            callback();
        }
    }
}
