﻿using System.Text.Json;
using System.Text.Json.Nodes;

using FLG.Cs.Datamodel;


namespace FLG.Cs.Serialization {
    internal class JsonSerializer : Serializer {
        internal const string SAVE_EXTENSION = ".jsave";
        protected override string GetSaveExtension() => SAVE_EXTENSION;
        protected override ESerializerType GetSerializerType() => ESerializerType.JSON;

        private JsonObject _properties;

        internal JsonSerializer(SerializerManager manager) : base(manager) {
            _properties = new();
        }

        public sealed override void Serialize(ISaveFile saveFile)
        {
            _properties = new();
            SaveHeader(saveFile);
            SerializeSerializables();

            var filepath = saveFile.Path;
            var options = new JsonWriterOptions { Indented = true };
            using var filestream = File.Create(filepath);
            using Utf8JsonWriter writer = new(filestream, options);
            _properties.WriteTo(writer);
        }

        public sealed override void Deserialize(ISaveFile saveFile)
        {
            var filepath = saveFile.Path;
            using var filestream = File.OpenRead(filepath);
            _properties = new();
            _properties = System.Text.Json.JsonSerializer.Deserialize<JsonObject>(filestream);
            LoadHeader();
            DeserializeSerializables();
        }

        internal sealed override SaveFileHeader DeserializeHeaderOnly(string filepath)
        {
            using var filestream = File.OpenRead(filepath);
            _properties = new();
            _properties = System.Text.Json.JsonSerializer.Deserialize<JsonObject>(filestream);
            return LoadHeader();
        }

        #region Primitive Types
        public override void SaveBool(bool value, string id)
        {
            _properties[id] = value;
        }
        public override bool LoadBool(string id)
        {
            return _properties[id].GetValue<bool>();
        }

        public override void SaveUint(uint value, string id)
        {
            _properties[id] = value;
        }
        public override uint LoadUint(string id)
        {
            return _properties[id].GetValue<uint>();
        }

        public override void SaveInt(int value, string id)
        {
            _properties[id] = value;
        }
        public override int LoadInt(string id)
        {
            return _properties[id].GetValue<int>();
        }

        public override void SaveLong(long value, string id)
        {
            _properties[id] = value;
        }
        public override long LoadLong(string id)
        {
            return _properties[id].GetValue<long>();
        }

        public override void SaveFloat(float value, string id)
        {
            _properties[id] = value;
        }
        public override float LoadFloat(string id)
        {
            return _properties[id].GetValue<float>();
        }

        public override void SaveDouble(double value, string id)
        {
            _properties[id] = value;
        }
        public override double LoadDouble(string id)
        {
            return _properties[id].GetValue<double>();
        }

        public override void SaveString(string value, string id)
        {
            _properties[id] = value;
        }
        public override string LoadString(string id)
        {
            return _properties[id].GetValue<string>();
        }
        #endregion Primitive Types

        #region Complex Types
        public override void SaveDateTime(DateTime value, string id)
        {
            _properties[id] = value;
        }
        public override DateTime LoadDateTime(string id)
        {
            return _properties[id].GetValue<DateTime>();
        }
        #endregion Complex Types
    }
}
