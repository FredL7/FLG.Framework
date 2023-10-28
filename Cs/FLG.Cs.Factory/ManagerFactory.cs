using FLG.Cs.Serialization;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI;

namespace FLG.Cs.Factory {
    public static class ManagerFactory {
        #region ISerializer
        public static void CreateBinarySerializer(string saveDir)
        {
            ISerializer serializer = new BinarySerializer(saveDir);
            SingletonManager.Instance.Register(serializer);
        }
        public static void CreateXMLSerializer(string saveDir)
        {
            ISerializer serializer = new XmlSerializer(saveDir);
            SingletonManager.Instance.Register(serializer);
        }
        public static void CreateJSONSerializer(string saveDir)
        {
            ISerializer serializer = new JsonSerializer(saveDir);
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
