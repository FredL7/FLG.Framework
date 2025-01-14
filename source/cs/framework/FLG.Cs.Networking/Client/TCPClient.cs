using System.Net.Sockets;

using FLG.Cs.Datamodel.Logger;

using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking.Client {
    internal class TCPClient(IConnection connection, IReceiver receiver, ThreadManager threadManager) : TCP(connection, receiver, threadManager) {
        public void Connect(string ip, int port)
        {

            _socket = new TcpClient
            {
                ReceiveBufferSize = Constants.DATA_BUFFER_SIZE,
                SendBufferSize = Constants.DATA_BUFFER_SIZE
            };

            _receiveBuffer = new byte[_receiveBuffer.Length];
            _socket.BeginConnect(ip, port, ConnectCallback, _socket);
        }

        private void ConnectCallback(IAsyncResult result)
        {
            if (_socket == null)
            {
                Locator.Instance.Get<ILogManager>().Debug("Tcp not properly initialized");
                return;
            }

            _socket.EndConnect(result);
            if (!_socket.Connected)
            {
                return;
            }

            _stream = _socket.GetStream();
            _receivedData = new Packet();
            _stream.BeginRead(_receiveBuffer, 0, Constants.DATA_BUFFER_SIZE, ReceiveCallback, null);
        }
    }
}
