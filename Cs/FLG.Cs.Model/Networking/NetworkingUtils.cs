using System.Net;
using System.Net.Sockets;


namespace FLG.Cs.Model {
    public static class NetworkingUtils {
        public static string GetHostname() => Dns.GetHostName();

        public static IPAddress GetIPAddress() => GetIPAddresses()[0];

        public static IPAddress[] GetIPv4Addresses()
        {
            var ipAddresses = GetIPAddresses();
            List<IPAddress> addresses = new List<IPAddress>();
            foreach (var ip in ipAddresses)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    addresses.Add(ip);
                }
            }

            return addresses.ToArray();
        }

        public static IPAddress[] GetIPAddresses()
        {
            string host = GetHostname();
            IPHostEntry entry = Dns.GetHostEntry(host);
            IPAddress[] addr = entry.AddressList;
            return addr;
        }
    }
}
