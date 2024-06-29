﻿using System.Net;
using System.Net.Sockets;

using FLG.Cs.Datamodel;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    internal class TCPConnexion {
        private readonly int _id;
        public int Id { get => _id; }

        private readonly NetworkingManager _manager;
        private readonly Server _server;

        private TcpClient? _client;
        private NetworkStream? _stream;
        private Message _receivedData;
        private byte[] _receiveBuffer;

        public bool Available { get => _client == null; }
        public EndPoint? RemoteEndPoint { get => _client?.Client.RemoteEndPoint; }

        public TCPConnexion(int id, NetworkingManager manager, Server server)
        {
            _id = id;
            _manager = manager;
            _server = server;

            _receiveBuffer = Array.Empty<byte>();
            _receivedData = new();
        }

        public void Connect(TcpClient client)
        {
            _client = client;
            _client.ReceiveBufferSize = Message.DATA_BUFFER_SIZE;
            _client.SendBufferSize = Message.DATA_BUFFER_SIZE;

            _stream = _client.GetStream();
            _receivedData = new();
            _receiveBuffer = new byte[Message.DATA_BUFFER_SIZE];
            _stream.BeginRead(_receiveBuffer, 0, Message.DATA_BUFFER_SIZE, ReceiveCallback, null);
        }

        public void SendData(Message message)
        {
            try
            {
                if (_client != null && _stream != null)
                {
                    _stream.BeginWrite(message.ToArray(), 0, message.Length, null, null);
                }
                else
                {
                    Locator.Instance.Get<ILogManager>().Error("Can't send message, invalid client");
                }
            }
            catch (Exception e)
            {
                Locator.Instance.Get<ILogManager>().Error($"Error Sending data to player {_id} via TCP ({e.Message})");
            }
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

        private bool HandleData(byte[] data)
        {
            int length = 0;

            _receivedData.SetBytes(data);

            // Messages always start with int from Message.cs::Messages enum
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
                byte[] messageBytes = _receivedData.ReadBytes(length);
                _manager.ExecuteOnMainThread(() =>
                {
                    using Message message = new(messageBytes);
                    int id = message.ReadInt();
                    _server.MessageHandlers[id](id, message);
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
    }
}
