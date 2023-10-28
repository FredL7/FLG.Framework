using FLG.Cs.IO;
using FLG.Cs.Logger;

namespace FLG.Cs.Serialization {
    public abstract class Serializer : ISerializer {
        private const uint VERSION = 0;
        private const string ID_VERSION = "Version";
        private const string ID_NAME = "Name";
        private const string ID_DATECREATED = "DateCreated";
        private const string ID_DATELASTMODIFIED = "DateLastModifier";

        private readonly List<ISerializable> _serializableItems;
        public void AddSerializable(ISerializable serializable)
        {
            _serializableItems.Add(serializable);
        }

        private readonly string _saveDir;
        private readonly List<ISaveFile> _saveFiles;
        public IEnumerable<ISaveFile> GetSaveFiles() => _saveFiles;

        public Serializer(string saveDir)
        {
            _serializableItems = new();
            _saveDir = saveDir;
            _saveFiles = new();

            DiscoverSaveFiles();
        }

        protected abstract string GetSaveExtension();

        private void DiscoverSaveFiles()
        {
            if (!Directory.Exists(_saveDir))
            {
                System.IO.Directory.CreateDirectory(_saveDir);
                return;
            }

            // TODO Parent serializer container that has one reader specified by the user upon creation (factory) but can read any type
            List<string> saveFiles = IOUtils.GetFilePathsByExtension(_saveDir, GetSaveExtension());
            foreach (var file in saveFiles)
            {
                var header = DeserializeHeader(file);
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

            SaveUint(VERSION, ID_VERSION);
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
        #endregion

        public void Serialize(string filename)
        {
            ISaveFile saveFile = new SaveFile(filename, Path.Combine(_saveDir, filename + GetSaveExtension()));
            _saveFiles.Add(saveFile);
            Serialize(saveFile);
        }

        public abstract void Serialize(ISaveFile saveFile);
        protected void SerializeSerializables()
        {
            foreach (var serializableItem in _serializableItems)
                serializableItem.Serialize();
        }

        public abstract void Deserialize(ISaveFile saveFile);
        protected abstract SaveFileHeader DeserializeHeader(string filepath);
        protected void DeserializeSerializables()
        {
            foreach (var serializableItem in _serializableItems)
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

        public abstract void SaveString(string value, string id);
        public abstract string LoadString(string id);
        #endregion Promitive Types

        #region Complex Types
        public abstract void SaveDateTime(DateTime value, string id);
        public abstract DateTime LoadDateTime(string id);
        #endregion Complex Types
    }
}
