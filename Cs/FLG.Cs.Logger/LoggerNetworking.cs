using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Networking;
using FLG.Cs.Datamodel.Commands;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Logger {
    internal class LoggerNetworking(string identifier, string networkingId) : FLGLogger(identifier, networkingId) {
        private readonly List<ICommand> _waitingList = [];

        protected override void Log(string logEntry, ELogLevel severity)
        {
            // TODO: Better setup with _waitingList
            // Currently required because there a logs going out before the INetworkingManager is registered

            var command = new Command<ILogManager>(severity.ToLogMethod());
            command.AddParam(logEntry);
            command.AddParam(true);
            _waitingList.Add(command);

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
                foreach (var waitingCommand in _waitingList)
                {
                    network.SendCommand(waitingCommand);
                }
                _waitingList.Clear();
            }
        }
    }
}
