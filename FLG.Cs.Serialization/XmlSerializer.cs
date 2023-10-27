namespace FLG.Cs.Serialization {
    public class XmlSerializer : Serializer {
        public XmlSerializer(string saveDir) : base(saveDir) { }

        protected override void BeforeSerialize(string filepath)
        {
            throw new NotImplementedException();
        }

        protected override void AfterSerialize()
        {
            throw new NotImplementedException();
        }

        protected override void BeforeDeserialize(string filepath)
        {
            throw new NotImplementedException();
        }

        protected override void AfterDeserialize()
        {
            throw new NotImplementedException();
        }

        #region Primitive Types

        public override void SaveBool(bool value)
        {
            throw new NotImplementedException();
        }
        public override bool LoadBool()
        {
            throw new NotImplementedException();
        }

        public override void SaveUint(uint value)
        {
            throw new NotImplementedException();
        }
        public override uint LoadUint()
        {
            throw new NotImplementedException();
        }

        public override void SaveInt(int value)
        {
            throw new NotImplementedException();
        }
        public override int LoadInt()
        {
            throw new NotImplementedException();
        }

        public override void SaveFloat(float value)
        {
            throw new NotImplementedException();
        }
        public override float LoadFloat()
        {
            throw new NotImplementedException();
        }

        public override void SaveString(string value)
        {
            throw new NotImplementedException();
        }
        public override string LoadString()
        {
            throw new NotImplementedException();
        }
        #endregion Primitive Types

        #region Complex Types
        public override void SaveDateTime(DateTime value)
        {
            throw new NotImplementedException();
        }
        public override DateTime LoadDateTime()
        {
            throw new NotImplementedException();
        }
        #endregion Complex Types
    }
}
