namespace FLG.Cs.Serialization {
    public class JsonSerializer : Serializer {
        public JsonSerializer(string saveDir) : base(saveDir) { }
        public sealed override void Serialize(ISaveFile saveFile)
        {
            throw new NotImplementedException();
        }

        public sealed override void Deserialize(ISaveFile saveFile)
        {
            throw new NotImplementedException();
        }

        #region Primitive Types

        public override void SaveBool(bool value, string id)
        {
            throw new NotImplementedException();
        }
        public override bool LoadBool(string id)
        {
            throw new NotImplementedException();
        }

        public override void SaveUint(uint value, string id)
        {
            throw new NotImplementedException();
        }
        public override uint LoadUint(string id)
        {
            throw new NotImplementedException();
        }

        public override void SaveInt(int value, string id)
        {
            throw new NotImplementedException();
        }
        public override int LoadInt(string id)
        {
            throw new NotImplementedException();
        }

        public override void SaveFloat(float value, string id)
        {
            throw new NotImplementedException();
        }
        public override float LoadFloat(string id)
        {
            throw new NotImplementedException();
        }

        public override void SaveString(string value, string id)
        {
            throw new NotImplementedException();
        }
        public override string LoadString(string id)
        {
            throw new NotImplementedException();
        }
        #endregion Primitive Types

        #region Complex Types
        public override void SaveDateTime(DateTime value, string id)
        {
            throw new NotImplementedException();
        }
        public override DateTime LoadDateTime(string id)
        {
            throw new NotImplementedException();
        }
        #endregion Complex Types
    }
}
