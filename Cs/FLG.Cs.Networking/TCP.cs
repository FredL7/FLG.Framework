using System.Net.Sockets;
using System.Net;

using FLG.Cs.Datamodel.Logger;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    internal abstract class TCP(IConnection connection, IReceiver receiver, ThreadManager threadManager) : IProtocol {
        protected Packet _receivedData = new();
        protected byte[] _receiveBuffer = new byte[Constants.DATA_BUFFER_SIZE];

        private readonly IConnection _connection = connection;
        private readonly IReceiver _receiver = receiver;
        private readonly ThreadManager _threadManager = threadManager;

        protected TcpClient? _socket;
        protected NetworkStream? _stream;

        public EndPoint? IP { get => _socket?.Client.RemoteEndPoint; }

        public void SendData(Packet packet)
        {
            try
            {
                if (_socket != null && _stream != null)
                {
                    _stream.BeginWrite(packet.ToArray(), 0, packet.Length, null, null);
                }
            }
            catch (Exception e)
            {
                Locator.Instance.Get<ILogManager>().Warn($"Error sending data via TCP: {e}");
            }
        }
        protected void ReceiveCallback(IAsyncResult result)
        {

            try
            {
                if (_socket != null && _stream != null)
                {
                    int byteLength = _stream.EndRead(result);
                    if (byteLength <= 0)
                    {
                        _connection.Disconnect();
                        return;
                    }

                    byte[] data = new byte[byteLength];
                    Array.Copy(_receiveBuffer, data, byteLength);

                    _receivedData.Reset(HandleData(data));
                    _stream.BeginRead(_receiveBuffer, 0, Constants.DATA_BUFFER_SIZE, ReceiveCallback, null);
                }
            }
            catch (System.IO.IOException e)
            {
                if (e.InnerException is System.Net.Sockets.SocketException socketEx && socketEx.SocketErrorCode == System.Net.Sockets.SocketError.ConnectionReset)
                {
                    Locator.Instance.Get<ILogManager>().Warn($"Client {_connection.ID} forcibly closed the connection");
                }
                else
                {
                    Locator.Instance.Get<ILogManager>().Debug($"Unexpected IOException: {e}");
                }
                _connection.Disconnect();
            }
            catch (Exception e)
            {
                Locator.Instance.Get<ILogManager>().Debug($"Error receiving TCP data: {e}");
                _connection.Disconnect();
            }
        }

        private bool HandleData(byte[] data)
        {
            int packetLength = 0;
            _receivedData.SetBytes(data);
            if (_receivedData.UnreadLength >= 4)
            {
                packetLength = _receivedData.ReadInt();
                if (packetLength <= 0)
                {
                    // Received a complete packet
                    return true;
                }
            }

            while (packetLength > 0 && packetLength <= _receivedData.UnreadLength)
            {
                byte[] packetBytes = _receivedData.ReadBytes(packetLength);
                _threadManager.ExecuteOnMainThread(() =>
                {
                    using Packet packet = new(packetBytes);
                    int packetId = packet.ReadInt();
                    _receiver.HandlePacket(packetId, _connection.ID, packet);
                });

                packetLength = 0;
                if (_receivedData.UnreadLength >= 4)
                {
                    packetLength = _receivedData.ReadInt();
                    if (packetLength <= 0)
                    {
                        // Received a complete packet
                        return true;
                    }
                }
            }

            if (packetLength <= 1)
            {
                // Complete packet
                return true;
            }

            // Incomplete packet
            return false;
        }

        public void Disconnect()
        {
            _socket?.Close();
            _socket = null;
            _receivedData = new Packet();
            _receiveBuffer = [];
            _stream = null;
        }
    }
}
