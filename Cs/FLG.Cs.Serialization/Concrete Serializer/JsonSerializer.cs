using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace FLG.Cs.Serialization {
    public class JsonSerializer : Serializer {
        private JsonObject _properties;

        public JsonSerializer(string saveDir) : base(saveDir) {
            _properties = new();
        }

        protected override string GetSaveExtension() => ".jsonsave";

        public sealed override void Serialize(ISaveFile saveFile)
        {
            _properties = new();
            SaveHeader(saveFile);
            SerializeSerializables();

            var filepath = saveFile.GetPath();
            var options = new JsonWriterOptions { Indented = true };
            using var filestream = File.Create(filepath);
            using Utf8JsonWriter writer = new(filestream, options);
            _properties.WriteTo(writer);
        }

        public sealed override void Deserialize(ISaveFile saveFile)
        {
            var filepath = saveFile.GetPath();
            using var filestream = File.OpenRead(filepath);
            _properties = System.Text.Json.JsonSerializer.Deserialize<JsonObject>(filestream);
            LoadHeader();
            DeserializeSerializables();
        }

        protected sealed override SaveFileHeader DeserializeHeaderOnly(string filepath)
        {
            using var filestream = File.OpenRead(filepath);
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
