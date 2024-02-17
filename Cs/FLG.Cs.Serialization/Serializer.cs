using FLG.Cs.IDatamodel;

namespace FLG.Cs.Serialization {
    internal abstract class Serializer : ISerializer {
        private const string ID_VERSION = "Version";
        private const string ID_NAME = "Name";
        private const string ID_DATECREATED = "DateCreated";
        private const string ID_DATELASTMODIFIED = "DateLastModifier";

        private SerializerManager _manager;

        public Serializer(SerializerManager manager)
        {
            _manager = manager;
        }

        protected void SaveHeader(ISaveFile saveFile)
        {
            saveFile.UpdateDateLastModified();

            SaveUint(SerializerManager.VERSION, ID_VERSION);
            SaveString(saveFile.GetName(), ID_NAME);
            SaveDateTime(saveFile.GetDateCreated(), ID_DATECREATED);
            SaveDateTime(saveFile.GetDateLastModified(), ID_DATELASTMODIFIED);
        }

        protected SaveFileHeader LoadHeader()
        {
            uint version = LoadUint(ID_VERSION);
            string name = LoadString(ID_NAME);
            var dateCreated = LoadDateTime(ID_DATECREATED);
            var dateLastModified = LoadDateTime(ID_DATELASTMODIFIED);
            return new()
            {
                version = version,
                name = name,
                dateCreated = dateCreated,
                dateLastModified = dateLastModified
            };
        }

        protected abstract string GetSaveExtension();
        protected abstract ESerializerType GetSerializerType();

        public void Serialize(string filename)
        {
            ISaveFile saveFile = new SaveFile(filename, Path.Combine(_manager.SaveDir, filename + GetSaveExtension()), GetSerializerType());
            _manager.AddSaveFile(saveFile);
            Serialize(saveFile);
        }

        public abstract void Serialize(ISaveFile saveFile);
        protected void SerializeSerializables()
        {
            foreach (var serializableItem in _manager.GetSerializableItems())
                serializableItem.Serialize();
        }

        public abstract void Deserialize(ISaveFile saveFile);
        internal abstract SaveFileHeader DeserializeHeaderOnly(string filepath);
        protected void DeserializeSerializables()
        {
            foreach (var serializableItem in _manager.GetSerializableItems())
                serializableItem.Deserialize();
        }

        #region Primitive Types
        public abstract void SaveBool(bool value, string id);
        public abstract bool LoadBool(string id);
        public abstract void SaveUint(uint value, string id);
        public abstract uint LoadUint(string id);
        public abstract void SaveInt(int value, string id);
        public abstract int LoadInt(string id);
        public abstract void SaveLong(long value, string id);
        public abstract long LoadLong(string id);
        public abstract void SaveFloat(float value, string id);
        public abstract float LoadFloat(string id);
        public abstract void SaveDouble(double value, string id);
        public abstract double LoadDouble(string id);
        public abstract void SaveString(string value, string id);
        public abstract string LoadString(string id);
        #endregion Promitive Types

        #region Complex Types
        public abstract DateTime LoadDateTime(string id);
        public abstract void SaveDateTime(DateTime value, string id);
        #endregion Complex Types
    }
}
