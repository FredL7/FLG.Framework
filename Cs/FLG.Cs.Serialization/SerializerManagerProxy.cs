namespace FLG.Cs.Serialization {
    public class SerializerManagerProxy : ISerializerManager {
        public bool IsProxy() => true;
        public void OnServiceRegistered() { }
        public void OnServiceRegisteredFail() {  }

        public IEnumerable<ISaveFile> GetSaveFiles() { return new List<ISaveFile>(); }
        public void AddSerializable(ISerializable serializable) { }
        public void Serialize(string filename) { }
        public void Serialize(ISaveFile saveFile) { }
        public void Deserialize(ISaveFile saveFile) { }
        public void SaveBool(bool value, string id) { }
        public bool LoadBool(string id) => default;
        public void SaveUint(uint value, string id) { }
        public uint LoadUint(string id) => default;
        public void SaveInt(int value, string id) { }
        public int LoadInt(string id) => default;
        public void SaveLong(long value, string id) { }
        public long LoadLong(string id) => default;
        public void SaveFloat(float value, string id) { }
        public float LoadFloat(string id) => default;
        public void SaveDouble(double value, string id) { }
        public double LoadDouble(string id) => default;
        public void SaveString(string value, string id) { }
        public string LoadString(string id) => String.Empty;
        public void SaveDateTime(DateTime value, string id) { }
        public DateTime LoadDateTime(string id) => default;
    }
}
