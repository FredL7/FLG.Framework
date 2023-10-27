using FLG.Cs.IO;
using FLG.Cs.Logger;

namespace FLG.Cs.Serialization {
    public abstract class Serializer : ISerializer {
        private const uint VERSION = 0;

        private List<ISerializable> _serializableItems;
        public void AddSerializable(ISerializable serializable)
        {
            _serializableItems.Add(serializable);
        }

        private string _saveDir;
        private List<ISaveFile> _saveFiles;

        public Serializer(string saveDir)
        {
            _serializableItems = new();
            _saveDir = saveDir;
            _saveFiles = new();

            DiscoverSaveFiles();
        }

        private void DiscoverSaveFiles()
        {
            if (!Directory.Exists(_saveDir))
            {
                System.IO.Directory.CreateDirectory(_saveDir);
                return;
            }

            List<string> saveFiles = IOUtils.GetFilePathsByExtension(_saveDir, ".save");
            foreach (var file in saveFiles)
            {
                var header = LoadHeader();
                var version = header.version;
                if (version != VERSION)
                {
                    LogManager.Instance.Warn($"Save file version does not correspond. Got {version}, expected {VERSION}). For file at \"{file}\"");
                    //? Upgrade/Convert
                }
                else
                {
                    ISaveFile saveFile = new SaveFile(header.name, file, header.dateCreated, header.dateLastModified);
                    _saveFiles.Add(saveFile);
                }
            }
        }

        #region Header
        protected struct SaveFileHeader {
            public uint version;
            public string name;
            public DateTime dateCreated;
            public DateTime dateLastModified;
        }

        protected void SaveHeader(ISaveFile saveFile)
        {
            saveFile.UpdateDateLastModified();

            SaveUint(VERSION);
            SaveString(saveFile.GetName());
            SaveDateTime(saveFile.GetDateCreated());
            SaveDateTime(saveFile.GetDateLastModified());
        }

        protected SaveFileHeader LoadHeader()
        {
            uint version = LoadUint();
            string name = LoadString();
            var dateCreated = LoadDateTime();
            var dateLastModified = LoadDateTime();
            return new()
            {
                version = version,
                name = name,
                dateCreated = dateCreated,
                dateLastModified = dateLastModified
            };
        }
        #endregion

        public abstract void Serialize(ISaveFile saveFile);
        protected void SerializeSerializables()
        {
            foreach (var serializableItem in _serializableItems)
                serializableItem.Serialize();
        }

        public abstract void Deserialize(ISaveFile saveFile);
        protected void DeserializeSerializables()
        {
            foreach (var serializableItem in _serializableItems)
                serializableItem.Deserialize();
        }

        #region Primitive Types
        public abstract void SaveBool(bool value);
        public abstract bool LoadBool();

        public abstract void SaveUint(uint value);
        public abstract uint LoadUint();

        public abstract void SaveInt(int value);
        public abstract int LoadInt();

        public abstract void SaveFloat(float value);
        public abstract float LoadFloat();

        public abstract void SaveString(string value);
        public abstract string LoadString();
        #endregion Promitive Types

        #region Complex Types
        public abstract void SaveDateTime(DateTime value);
        public abstract DateTime LoadDateTime();
        #endregion Complex Types
    }
}
