namespace FLG.Cs.Datamodel {
    public interface ISerializer {
        public void Serialize(string filename);
        public void Serialize(ISaveFile saveFile);
        public void Deserialize(ISaveFile saveFile);

        #region Primitive Types
        public void SaveBool(bool value, string id);
        public bool LoadBool(string id);
        public void SaveUint(uint value, string id);
        public uint LoadUint(string id);
        public void SaveInt(int value, string id);
        public int LoadInt(string id);
        public void SaveLong(long value, string id);
        public long LoadLong(string id);
        public void SaveFloat(float value, string id);
        public float LoadFloat(string id);
        public void SaveDouble(double value, string id);
        public double LoadDouble(string id);
        public void SaveString(string value, string id);
        public string LoadString(string id);
        #endregion Primitive Types

        #region Complex Types
        public void SaveDateTime(DateTime value, string id);
        public DateTime LoadDateTime(string id);
        #endregion Complex Types
    }
}
