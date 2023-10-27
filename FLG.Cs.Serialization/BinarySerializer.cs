namespace FLG.Cs.Serialization {
    public class BinarySerializer : Serializer {
        private BinaryReader? _reader = null;
        private BinaryWriter? _writer = null;

        public BinarySerializer(string saveDir) : base(saveDir) { }

        public sealed override void Serialize(ISaveFile saveFile)
        {
            var filepath = saveFile.GetPath();
            using (_writer = new(File.Open(filepath, FileMode.Create)))
            {
                SaveHeader(saveFile);
                SerializeSerializables();
            }
        }

        public sealed override void Deserialize(ISaveFile saveFile)
        {
            var filepath = saveFile.GetPath();
            using (_reader = new(File.OpenRead(filepath)))
            {
                var header = LoadHeader();
                DeserializeSerializables();
            }
        }

        #region Primitive Types

        public override void SaveBool(bool value, string _)
        {
            _writer.Write(value);
        }
        public override bool LoadBool(string _)
        {
            return _reader.ReadBoolean();
        }

        public override void SaveUint(uint value, string _)
        {
            _writer.Write(value);
        }
        public override uint LoadUint(string _)
        {
            return _reader.ReadUInt32();
        }

        public override void SaveInt(int value, string _)
        {
            _writer.Write(value);
        }
        public override int LoadInt(string _)
        {
            return _reader.ReadInt32();
        }

        public override void SaveFloat(float value, string _)
        {
            _writer.Write(value);
        }
        public override float LoadFloat(string _)
        {
            return _reader.ReadSingle();
        }

        public override void SaveString(string value, string _)
        {
            // var count = value.Length;
            //SaveInt(count);
            _writer.Write(value);
        }
        public override string LoadString(string _)
        {
            return _reader.ReadString();
        }
        #endregion Primitive Types

        #region Complex Types
        // TODO: https://stackoverflow.com/questions/15919598/serialize-datetime-as-binary
        public override void SaveDateTime(DateTime value, string _)
        {
            throw new NotImplementedException();
        }
        public override DateTime LoadDateTime(string _)
        {
            throw new NotImplementedException();
        }
        #endregion Complex Types
    }
}
