namespace FLG.Cs.Networking {
    // Server to client
    internal enum ServerPackets {
        WELCOME,
        DISCONNECTED,
        COMMAND
    }

    // Client to server
    internal enum ClientPackets {
        WELCOME_RECEIVED,
        COMMAND
    }
}
