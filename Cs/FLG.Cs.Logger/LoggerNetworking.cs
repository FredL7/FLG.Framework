using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Networking;
using FLG.Cs.Datamodel.Commands;

using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Logger {
    internal class LoggerNetworking(string identifier, string networkingId) : FLGLogger(identifier, networkingId) {
        public override void LogEntry(LogEntry entry)
        {
            // Will drop logs until networking manager is initialized
            // Use another logger to catch the missed logs
            INetworkingManager? network;
            try
            {
                network = Locator.Instance.Get<INetworkingManager>();
            }
            catch (Exception e)
            {
                if (e.Message.StartsWith("Service not registered"))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }

            if (network.IsConnected)
            {
                var command = new Command<ILogManager>("LogEntry");
                command.AddParam(entry);
                network.SendCommand(command);
            }
        }
    }
}
