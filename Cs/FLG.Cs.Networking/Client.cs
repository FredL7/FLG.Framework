using System.Data;
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

        private readonly MessagesHandler _messagesHandler;

        public Client(string ip, int port, NetworkingManager manager)
        {
            _manager = manager;
            _ip = ip;
            _port = port;

            _messagesHandler = new(new()
            {
                { (int)Messages.WELCOME, WelcomeHandler },
                { (int)Messages.HEARTBEAT, HeartbeatHandler },
                { (int)Messages.COMMAND, CommandHandler },
            });

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

                    _receivedData.Reset(MessagesHandler.HandleData(data, _receivedData, _manager, _messagesHandler.MessageHandlers));
                    _stream.BeginRead(_receiveBuffer, 0, Message.DATA_BUFFER_SIZE, ReceiveCallback, null);
                }
            }
            catch (Exception e)
            {
                Locator.Instance.Get<ILogManager>().Error($"Error receiving TCP data: {e}");
                // TODO: Disconnect
            }
        }
        #endregion Connexion

        #region Messaging
        public void SendCommand(ICommand command)
        {
            string commandMessage = command.ToMessageString();
            using Message message = new((int)Messages.COMMAND);
            message.Write(commandMessage);
            Locator.Instance.Get<ILogManager>().Debug($"Sending command message to server ({commandMessage})");
            SendTcpData(message);
        }
        private void SendTcpData(Message message)
        {
            message.WriteId(_id);
            message.WriteLength();
            Locator.Instance.Get<ILogManager>().Debug($"Sending message to server ({message})");
            SendData(message);
        }

        private void SendData(Message message)
        {
            // Do not call directly, call SendTcpData instead
            try
            {
                _stream?.BeginWrite(message.ToArray(), 0, message.Length, null, null);
            }
            catch (Exception e)
            {
                Locator.Instance.Get<ILogManager>().Error($"Error sending data to server via TCP: {e}");
            }
        }
        #endregion Messaging


        #region Message Handlers
        private void WelcomeHandler(int serverId, Message message)
        {
            var logger = Locator.Instance.Get<ILogManager>();
            logger.Debug($"Receiving welcome message from server ({serverId})");
            string msg = message.ReadString();
            int id = message.ReadInt();

            _id = id;
            logger.Debug($"Received welcome message from server \"{msg}\", my id={_id}");

            {
                using Message callbackMessage = new((int)Messages.WELCOME);
                callbackMessage.Write(_id, "clientreceivedId");
                logger.Debug("Sending Welcome message to server");
                SendTcpData(callbackMessage);
            }
        }

        private void HeartbeatHandler(int serverId, Message message)
        {
            throw new NotImplementedException();
        }

        private void CommandHandler(int serverId, Message message)
        {
            throw new NotImplementedException();
        }
        #endregion Message Handlers
    }
}
