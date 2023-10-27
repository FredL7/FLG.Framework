using FLG.Cs.ServiceLocator;

namespace FLG.Cs.Serialization {
    public interface ISerializer : IServiceInstance {
        public void AddSerializable(ISerializable serializable);
        public void Serialize(ISaveFile saveFile);
        public void Deserialize(ISaveFile saveFile);

        // public IEnumerable<>

        #region Primitive Types
        public void SaveBool(bool value);
        public bool LoadBool();

        public void SaveUint(uint value);
        public uint LoadUint();

        public void SaveInt(int value);
        public int LoadInt();

        public void SaveFloat(float value);
        public float LoadFloat();

        public void SaveString(string value);
        public string LoadString();
        #endregion Primitive Types

        #region Complex Types
        public void SaveDateTime(DateTime value);
        public DateTime LoadDateTime();
        #endregion Complex Types
    }
}
