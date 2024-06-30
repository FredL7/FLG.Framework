﻿namespace FLG.Cs.Datamodel {
    public interface INetworkingManager : IServiceInstance, IGameLoopObject {
        public int MaxServerConnexions { get; }
        public int ServerPort { get; }

        public void InitializeClient(string ip, int port);
        public void InitializeServer(int port, int maxConnexions = 0);

        public void SendCommand(ICommand command);
    }
}
