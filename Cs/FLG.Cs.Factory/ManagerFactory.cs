using FLG.Cs.Serialization;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI;

namespace FLG.Cs.Factory {
    public static class ManagerFactory {
        #region ISerializer
        public static void CreateBinarySerializer(string saveDir)
        {
            ISerializerManager serializer = SerializerManager.CreateBinarySerializer(saveDir);
            SingletonManager.Instance.Register(serializer);
        }
        public static void CreateJSONSerializer(string saveDir)
        {
            ISerializerManager serializer = SerializerManager.CreateJsonSerializer(saveDir);
            SingletonManager.Instance.Register(serializer);
        }
        public static void CreateXmlSerializer(string saveDir)
        {
            ISerializerManager serializer = SerializerManager.CreateXmlSerializer(saveDir);
            SingletonManager.Instance.Register(serializer);
        }
        #endregion ISerializer

        #region IUIManager
        public static void CreateUIManager()
        {
            IUIManager manager = new UIManager();
            SingletonManager.Instance.Register(manager);
        }
        #endregion IUIManager
    }
}
