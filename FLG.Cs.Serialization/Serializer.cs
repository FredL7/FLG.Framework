using FLG.Cs.IO;
using FLG.Cs.Logger;

namespace FLG.Cs.Serialization {
    public abstract class Serializer : ISerializer {
        private const uint VERSION = 0;

        private List<ISerializable> _serializableItems;

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
                uint version = LoadUint();
                if (version != VERSION)
                {
                    LogManager.Instance.Warn($"Save file version does not correspond. Got {version}, expected {VERSION}). For file at \"{file}\"");
                    //? Upgrade/Convert
                }
                else
                {
                    string name = LoadString();
                    var dateCreated = LoadDateTime();
                    var dateModified = LoadDateTime();
                    ISaveFile saveFile = new SaveFile(name, file, dateCreated, dateModified);
                    _saveFiles.Add(saveFile);
                }
            }
        }

        public void AddSerializable(ISerializable serializable)
        {
            _serializableItems.Add(serializable);
        }

        public void Serialize(ISaveFile saveFile)
        {
            BeforeSerialize(saveFile.GetPath()  );
            saveFile.UpdateDateLastModified();

            SaveUint(VERSION);
            SaveDateTime(saveFile.GetDateCreated());
            SaveDateTime(saveFile.GetDateLastModified());

            foreach (var serializableItem in _serializableItems)
                serializableItem.Serialize();
            AfterSerialize();
        }

        public void Deserialize(ISaveFile saveFile)
        {
            BeforeDeserialize(saveFile.GetPath());

            var version = LoadUint();
            if (version != VERSION)
            {
                LogManager.Instance.Warn($"Save file {saveFile.GetName()} uses older version (got {version}, expected {VERSION}).");
                return;
                //? AfterDeserialize()
            }
            var _ = LoadDateTime();
            var __ = LoadDateTime();

            foreach(var serializableItem in _serializableItems)
                serializableItem.Deserialize();
            AfterDeserialize();
        }

        protected abstract void BeforeSerialize(string filepath);
        protected abstract void AfterSerialize();
        protected abstract void BeforeDeserialize(string filepath);
        protected abstract void AfterDeserialize();

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
