using System.Net.Sockets;
using System.Text;

using FLG.Cs.Datamodel;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    public class TCPClient {
        private TcpClient _socketConnection;
        private Thread _clientReceiveThread;

        private string _host;
        private int _port;
        private string _source;

        public void Connect(string source, string host, int port)
        {
            _source = source;
            _host = host;
            _port = port;

            try
            {
                _clientReceiveThread = new(new ThreadStart(ListenForData)) { IsBackground = true };
                _clientReceiveThread.Start();
            }
            catch (Exception e)
            {
                var logger = Locator.Instance.Get<ILogManager>();
                logger.Warn(e.Message);
            }
        }

        private void ListenForData()
        {
            try
            {
                var logger = Locator.Instance.Get<ILogManager>();

                _socketConnection = new TcpClient(_host, _port);
                var bytes = new byte[1024];
                while (true)
                {
                    using NetworkStream stream = _socketConnection.GetStream();
                    int length;
                    while ((length = stream.Read(bytes, 0, bytes.Length)) > 0)
                    {
                        var incomingData = new byte[length];
                        Array.Copy(bytes, 0, incomingData, 0, length);
                        string serverMessage = Encoding.ASCII.GetString(incomingData); // TODO: UTF8?

                        logger.Debug($"CLIENT {_source}: Server message received as: {serverMessage}");
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
            if (_socketConnection == null)
            {
                var logger = Locator.Instance.Get<ILogManager>();
                logger.Debug($"CLIENT {_source}: Can't send message \"{msg}\", connection not established");
                return;
            }

            try
            {
                NetworkStream stream = _socketConnection.GetStream();
                if (stream.CanWrite)
                {
                    var msgAsByteArray = Encoding.ASCII.GetBytes(msg); // TODO: UTF8?
                    stream.Write(msgAsByteArray, 0, msgAsByteArray.Length);

                    var logger = Locator.Instance.Get<ILogManager>();
                    logger.Debug($"CLIENT {_source}: Message \"{msg}\" - should be received by server");
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
