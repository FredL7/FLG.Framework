using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using FLG.Cs.Datamodel;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    public class TCPServer {
        private TcpListener _tcpListener;
        private Thread _tcpListenerThread, _cmdHandlerThread;
        private TcpClient _connectedTcpClient;

        private string _host;
        private int _port;

        private List<string> _tcpListenerCmd, _cmdHandlerCmd;

        public void HandleCommands()
        {
            while (true)
            {
                if (_cmdHandlerCmd.Count > 0)
                {
                    foreach (var cmd in _cmdHandlerCmd)
                    {
                        // TODO: Log for testing
                    }
                    _cmdHandlerCmd.Clear();
                }
                if (_tcpListenerCmd.Count > 0)
                {
                    // Todo: Thread safety, use lock?
                    _cmdHandlerCmd = new(_tcpListenerCmd);
                    _tcpListenerCmd.Clear();
                }
            }
        }

        public void Connect(string host, int port)
        {
            _host = host;
            _port = port;

            _tcpListenerCmd = new();
            _cmdHandlerCmd = new();

            _cmdHandlerThread = new(new ThreadStart(HandleCommands)) { IsBackground = true };
            _cmdHandlerThread.Start();

            _tcpListenerThread = new(new ThreadStart(ListenForIncommingRequests)) { IsBackground = true };
            _tcpListenerThread.Start();
        }

        private void ListenForIncommingRequests()
        {
            try
            {
                _tcpListener = new(IPAddress.Parse(_host), _port); // TODO: Multiple Listeners
                _tcpListener.Start();

                var logger = Locator.Instance.Get<ILogManager>();
                logger.Debug("Server is listening");

                var bytes = new byte[1024];
                while (true)
                {
                    using (_connectedTcpClient = _tcpListener.AcceptTcpClient())
                    {
                        using NetworkStream stream = _connectedTcpClient.GetStream();
                        int length;
                        while ((length = stream.Read(bytes, 0, bytes.Length)) > 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            string clientMessage = Encoding.ASCII.GetString(incommingData);

                            logger.Debug($"SERVER: client message received as: {clientMessage}");

                            _tcpListenerCmd.Add(clientMessage); // TODO: Threadsafe add, use lock?
                        }
                    }
                }
            }
            catch (SocketException e)
            {
                var logger = Locator.Instance.Get<ILogManager>();
                logger.Warn(e.Message);
            }
        }

        public void SendMessage(string msg)
        {
            if (_connectedTcpClient == null)
            {
                var logger = Locator.Instance.Get<ILogManager>();
                logger.Debug($"SERVER: Can't send message \"{msg}\", connection not established");
                return;
            }

            try
            {
                NetworkStream stream = _connectedTcpClient.GetStream();
                if (stream.CanWrite)
                {
                    var msgAsByteArray = Encoding.ASCII.GetBytes(msg); // TODO: UTF8?
                    stream.Write(msgAsByteArray, 0, msgAsByteArray.Length);

                    var logger = Locator.Instance.Get<ILogManager>();
                    logger.Debug("SERVER: Message \"{msg}\" - should be received by client");
                }
            }
            catch (SocketException e)
            {
                var logger = Locator.Instance.Get<ILogManager>();
                logger.Warn(e.Message);
            }
        }
    }
}
