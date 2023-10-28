using FLG.Cs.ServiceLocator;

namespace FLG.Cs.Serialization.Tests {
    internal class SerializableItem : ISerializable {
        private const string ID_BOOL = "bool";
        private const string ID_UINT = "uint";
        private const string ID_INT = "int";
        private const string ID_FLOAT = "float";
        private const string ID_STRING = "string";
        private const string ID_DATE = "date";

        private bool _boolValue;
        private uint _uintValue;
        private int _intValue;
        private float _floatValue;
        private string _stringValue;
        private DateTime _dateValue;

        public bool GetBoolValue() => _boolValue;
        public uint GetUintValue() => _uintValue;
        public int GetIntValue() => _intValue;
        public float GetFloatValue() => _floatValue;
        public string GetStringValue() => _stringValue;
        public DateTime GetDateValue() => _dateValue;

        public SerializableItem(bool boolValue, uint uintValue, int intValue, float floatValue, string stringValue, DateTime dateValue)
        {
            _boolValue = boolValue;
            _uintValue = uintValue;
            _intValue = intValue;
            _floatValue = floatValue;
            _stringValue = stringValue;
            _dateValue = dateValue;
        }

        public void Set(bool boolValue, uint uintValue, int intValue, float floatValue, string stringValue, DateTime dateValue)
        {
            _boolValue = boolValue;
            _uintValue = uintValue;
            _intValue = intValue;
            _floatValue = floatValue;
            _stringValue = stringValue;
            _dateValue = dateValue;
        }

        public void Serialize()
        {
            ISerializer serializer = SingletonManager.Instance.Get<ISerializer>();
            serializer.SaveBool(_boolValue, ID_BOOL);
            serializer.SaveUint(_uintValue, ID_UINT);
            serializer.SaveInt(_intValue, ID_INT);
            serializer.SaveFloat(_floatValue, ID_FLOAT);
            serializer.SaveString(_stringValue, ID_STRING);
            serializer.SaveDateTime(_dateValue, ID_DATE);
        }

        public void Deserialize()
        {
            ISerializer serializer = SingletonManager.Instance.Get<ISerializer>();
            _boolValue = serializer.LoadBool(ID_BOOL);
            _uintValue = serializer.LoadUint(ID_UINT);
            _intValue = serializer.LoadInt(ID_INT);
            _floatValue = serializer.LoadFloat(ID_FLOAT);
            _stringValue = serializer.LoadString(ID_STRING);
            _dateValue = serializer.LoadDateTime(ID_DATE);
        }
    }
}
