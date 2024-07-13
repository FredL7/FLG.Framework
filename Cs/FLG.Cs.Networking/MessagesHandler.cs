// using FLG.Cs.Datamodel;
// using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Networking {
    internal class MessagesHandler {
        internal delegate void MessageHandler(int sourceId, Message message);
        private readonly Dictionary<int, MessageHandler> _messageHandlers;
        public Dictionary<int, MessageHandler> MessageHandlers { get => _messageHandlers; }

        public MessagesHandler(Dictionary<int, MessageHandler> messageHandlers)
        {
            _messageHandlers = messageHandlers;
        }

        internal static bool HandleData(byte[] data,
            Message receivedData, NetworkingManager manager, Dictionary<int, MessageHandler> messagesHandler)
        {
            int length = 0;

            receivedData.SetBytes(data);

            // var logger = Locator.Instance.Get<ILogManager>();
            // logger.Debug("Handling Data");

            if (receivedData.UnreadLength >= Message.INT_LENGTH)
            {
                length = receivedData.ReadInt();
                // logger.Debug($"Before: Length={length}");
                if (length <= 0)
                {
                    // logger.Debug("Before: All data handled");
                    return true;
                }
            }

            // logger.Debug("Data left to handle");

            while (length > 0 && length <= receivedData.UnreadLength)
            {
                byte[] messageBytes = receivedData.ReadBytes(length);
                manager.ExecuteOnMainThread(() =>
                {
                    using Message message = new(messageBytes);
                    int sourceId = message.ReadInt();
                    int handlerId = message.ReadInt();
                    // logger.Debug($"Send to main thread handler {handlerId} with source {sourceId}");
                    messagesHandler[handlerId](sourceId, message);
                });

                length = 0;
                if (receivedData.UnreadLength >= Message.INT_LENGTH)
                {
                    length = receivedData.ReadInt();
                    // logger.Debug($"While: Length={length}");
                    if (length <= 0)
                    {
                        // logger.Debug("While: all data handled");
                        return true;
                    }
                }
            }

            if (length <= 1)
            {
                // logger.Debug("After: Length = 1");
                return true;
            }

            // logger.Debug("Return false, message incomplete");
            return false;
        }
    }
}
