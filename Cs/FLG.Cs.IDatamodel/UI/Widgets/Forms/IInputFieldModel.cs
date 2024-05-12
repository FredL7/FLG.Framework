namespace FLG.Cs.IDatamodel {
    public interface IInputFieldModel {
        public bool SetValue(object value);

        public string GetValueAsString();
        public int GetValueAsInt();
        public float GetValueAsFloat();
        public bool GetValueAsBool();

        public void Clear();
    }
}
