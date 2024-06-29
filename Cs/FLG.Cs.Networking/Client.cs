using System.Net.Sockets;

using FLG.Cs.Datamodel;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    internal class Client {
        private readonly NetworkingManager _manager;

        private int _id;

        private readonly string _ip;
        private readonly int _port;

        private TcpClient? _socket;
        private NetworkStream? _stream;
        private Message _receivedData;
        private byte[] _receiveBuffer;

        internal delegate void MessageHandler(Message message);
        private readonly Dictionary<int, MessageHandler> _messageHandlers;
        internal Dictionary<int, MessageHandler> MessageHandlers { get => _messageHandlers; }

        public Client(string ip, int port, NetworkingManager manager)
        {
            _manager = manager;
            _ip = ip;
            _port = port;

            _messageHandlers = new()
            {
                { (int)Messages.WELCOME, WelcomeHandler },
                { (int)Messages.HEARTBEAT, HeartbeatHandler },
                { (int)Messages.COMMAND, CommandHandler },
            };

            _receiveBuffer = Array.Empty<byte>();
            _receivedData = new Message();
            _manager = manager;
        }

        #region Connexion
        public void ConnectToServer()
        {
            _socket = new()
            {
                ReceiveBufferSize = Message.DATA_BUFFER_SIZE,
                SendBufferSize = Message.DATA_BUFFER_SIZE,
            };

            _receiveBuffer = new byte[Message.DATA_BUFFER_SIZE];
            _socket.BeginConnect(_ip, _port, ConnectCallback, _socket);
        }

        private void ConnectCallback(IAsyncResult result)
        {
            if (_socket == null)
            {
                Locator.Instance.Get<ILogManager>().Error("Error on connect callback, TcpClient is null");
                return;
            }

            _socket.EndConnect(result);
            if (!_socket.Connected)
            {
                Locator.Instance.Get<ILogManager>().Error("Error on connect callback, socket not connected");
                return;
            }

            _stream = _socket.GetStream();
            _receivedData = new Message();
            _stream.BeginRead(_receiveBuffer, 0, Message.DATA_BUFFER_SIZE, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                if (_stream != null)
                {
                    int byteLength = _stream.EndRead(result);
                    if (byteLength <= 0)
                    {
                        // TODO: Disconnect
                        return;
                    }

                    byte[] data = new byte[byteLength];
                    Array.Copy(_receiveBuffer, data, byteLength);

                    _receivedData.Reset(HandleData(data));
                    _stream.BeginRead(_receiveBuffer, 0, Message.DATA_BUFFER_SIZE, ReceiveCallback, null);
                }
            }
            catch (Exception e)
            {
                Locator.Instance.Get<ILogManager>().Error($"Error receiving TCP data: {e}");
                // TODO: Disconnect
            }
        }

        private bool HandleData(byte[] data) // Copied in TCPConnexion (except the ExecuteOnMainThread inner)
        {
            int length = 0;

            _receivedData.SetBytes(data);

            if (_receivedData.UnreadLength >= Message.INT_LENGTH)
            {
                length = _receivedData.ReadInt();
                if (length <= 0)
                {
                    return true;
                }
            }

            while (length > 0 && length <= _receivedData.UnreadLength)
            {
                byte[] packetBytes = _receivedData.ReadBytes(length);
                _manager.ExecuteOnMainThread(() =>
                {
                    using Message message = new(packetBytes);
                    int id = message.ReadInt();
                    _messageHandlers[id](message);
                });

                length = 0;
                if (_receivedData.UnreadLength >= Message.INT_LENGTH)
                {
                    length = _receivedData.ReadInt();
                    if (length <= 0)
                    {
                        return true;
                    }
                }
            }

            if (length <= 1)
            {
                return true;
            }

            return false;
        }
        #endregion Connexion

        #region Messaging
        private void SendTcpData(Message message)
        {
            message.WriteLength();
            SendData(message);
        }

        private void SendData(Message message)
        {
            // Do not call directly, call SendTcpData instead
            try
            {
                // Maybe add message.WriteLength to the ToArray() function?
                _stream?.BeginWrite(message.ToArray(), 0, message.Length, null, null);
            }
            catch (Exception e)
            {
                Locator.Instance.Get<ILogManager>().Error($"Error sending data to server via TCP: {e}");
            }
        }
        #endregion Messaging


        #region Message Handlers
        private void WelcomeHandler(Message message)
        {
            string msg = message.ReadString();
            int id = message.ReadInt();

            _id = id;
            Locator.Instance.Get<ILogManager>().Debug($"Receiving Message from server \"{msg}\", my id={_id}");

            {
                using Message callbackMessage = new((int)Messages.WELCOME);
                callbackMessage.Write(_id);
                callbackMessage.Write("username"); // TODO: Add username
                Locator.Instance.Get<ILogManager>().Debug("Sending Welcome message to server (WELCOME, CLIENT ID, USERNAME)");
                SendTcpData(message);
            }
        }

        private void HeartbeatHandler(Message message)
        {
            throw new NotImplementedException();
        }

        private void CommandHandler(Message message)
        {
            throw new NotImplementedException();
        }
        #endregion Message Handlers
    }
}
