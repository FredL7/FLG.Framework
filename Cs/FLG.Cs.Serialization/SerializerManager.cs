﻿using FLG.Cs.Datamodel;
using FLG.Cs.IO;
using FLG.Cs.ServiceLocator;

using File = FLG.Cs.IO.File;


namespace FLG.Cs.Serialization {
    public class SerializerManager : ISerializerManager {
        internal const uint VERSION = 0;

        private Serializer _writeSerializer;
        private readonly Serializer _binSerializer;
        private readonly Serializer _jsonSerializer;
        private readonly Serializer _xmlSerializer;

        private readonly string _saveDir;
        internal string SaveDir { get => _saveDir; }
        private readonly List<ISaveFile> _saveFiles;
        public IEnumerable<ISaveFile> GetSaveFiles() => _saveFiles;
        internal void AddSaveFile(ISaveFile saveFile) { _saveFiles.Add(saveFile); }

        private readonly List<ISerializable> _serializableItems;
        public void AddSerializable(ISerializable serializable) { _serializableItems.Add(serializable); }
        public IEnumerable<ISerializable> GetSerializableItems() => _serializableItems;

        public SerializerManager(PreferencesSerialization prefs)
        {
            _binSerializer = new BinarySerializer(this);
            _jsonSerializer = new JsonSerializer(this);
            _xmlSerializer = new XmlSerializer(this);
            _writeSerializer = prefs.serializerType switch
            {
                ESerializerType.BIN => _binSerializer,
                ESerializerType.JSON => _jsonSerializer,
                ESerializerType.XML => _xmlSerializer,
                _ => _binSerializer,
            };

            _saveDir = prefs.savesDir;
            _saveFiles = new();

            _serializableItems = new();
        }

        #region IServiceInstance
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered() {
            Locator.Instance.Get<ILogManager>().Debug("Serialization Manager Registered");
            DiscoverSaveFile();
        }
        #endregion IServiceInstance

        public void SetSerializerBinary() { _writeSerializer = _binSerializer; }
        public void SetSerializerJson() { _writeSerializer = _jsonSerializer; }
        public void SetSerializerXml() { _writeSerializer = _xmlSerializer; }

        private void DiscoverSaveFile()
        {
            if (!Directory.Exists(_saveDir))
            {
                System.IO.Directory.CreateDirectory(_saveDir);
                return;
            }

            string[] exts = { BinarySerializer.SAVE_EXTENSION, JsonSerializer.SAVE_EXTENSION, XmlSerializer.SAVE_EXTENSION };
            List<File> saveFiles = IOUtils.GetFilePathsByExtensions(_saveDir, exts);
            foreach (var file in saveFiles)
            {
                Serializer serializer;
                ESerializerType serializerType;
                switch (file.extension)
                {
                    case BinarySerializer.SAVE_EXTENSION:
                        serializer = _binSerializer;
                        serializerType = ESerializerType.BIN;
                        break;
                    case JsonSerializer.SAVE_EXTENSION:
                        serializer = _jsonSerializer;
                        serializerType = ESerializerType.JSON;
                        break;
                    case XmlSerializer.SAVE_EXTENSION:
                        serializer = _xmlSerializer;
                        serializerType = ESerializerType.XML;
                        break;
                    default:
                        Locator.Instance.Get<ILogManager>().Warn($"Wrong extension for save file at \"{file}\"");
                        return;
                }

                var header = serializer.DeserializeHeaderOnly(file.fullpath);
                var version = header.version;
                if (version != VERSION)
                {

                    Locator.Instance.Get<ILogManager>().Warn($"Save file version does not correspond. Got {version}, expected {VERSION}). For file at \"{file}\"");
                    //? Upgrade/Convert
                }
                else
                {
                    ISaveFile saveFile = new SaveFile(header.name, file.fullpath, serializerType, header.dateCreated, header.dateLastModified);
                    _saveFiles.Add(saveFile);
                }
            }
        }

        public void Serialize(string filename) { _writeSerializer.Serialize(filename); }
        public void Serialize(ISaveFile saveFile) { _writeSerializer.Serialize(saveFile); }
        public void Deserialize(ISaveFile saveFile)
        {
            ESerializerType t = saveFile.Type;
            switch (t)
            {
                case ESerializerType.BIN: _binSerializer.Deserialize(saveFile); break;
                case ESerializerType.JSON: _jsonSerializer.Deserialize(saveFile); break;
                case ESerializerType.XML: _xmlSerializer.Deserialize(saveFile); break;
                default:
                    Locator.Instance.Get<ILogManager>().Warn($"Could not deserialize save file {saveFile.Name}, unrecognized type {t}");
                    break;
            }
        }

        #region Primitive Types
        public void SaveBool(bool value, string id) => _writeSerializer.SaveBool(value, id);
        public bool LoadBool(string id) => _writeSerializer.LoadBool(id);
        public void SaveUint(uint value, string id) => _writeSerializer.SaveUint(value, id);
        public uint LoadUint(string id) => _writeSerializer.LoadUint(id);
        public void SaveInt(int value, string id) => _writeSerializer.SaveInt(value, id);
        public int LoadInt(string id) => _writeSerializer.LoadInt(id);
        public void SaveLong(long value, string id) => _writeSerializer.SaveLong(value, id);
        public long LoadLong(string id) => _writeSerializer.LoadLong(id);
        public void SaveFloat(float value, string id) => _writeSerializer.SaveFloat(value, id);
        public float LoadFloat(string id) => _writeSerializer.LoadFloat(id);
        public void SaveDouble(double value, string id) => _writeSerializer.SaveDouble(value, id);
        public double LoadDouble(string id) => _writeSerializer.LoadDouble(id);
        public void SaveString(string value, string id) => _writeSerializer.SaveString(value, id);
        public string LoadString(string id) => _writeSerializer.LoadString(id);
        #endregion Primitive Types

        #region Complex Types
        public void SaveDateTime(DateTime value, string id) { _writeSerializer.SaveDateTime(value, id); }
        public DateTime LoadDateTime(string id) => _writeSerializer.LoadDateTime(id);
        #endregion Complex Types
    }
}
