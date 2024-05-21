namespace FLG.Cs.IDatamodel {
    public class SimpleStringModel : IInputFieldModel {
        private string _initialValue;
        private string _value;
        private Action? _clearUICallback;

        public SimpleStringModel(string initialValue = "")
        {
            _value = _initialValue = initialValue;
        }

        public bool SetValue(object value)
        {
            var transient = Convert.ToString(value);
            if (transient != null)
            {
                _value = transient;
                return true;
            }
            return false;
        }

        public string GetValueAsString() => _value;
        public int GetValueAsInt() => Convert.ToInt32(_value);
        public float GetValueAsFloat() => Convert.ToSingle(_value);
        public bool GetValueAsBool() => Convert.ToBoolean(_value);


        public void SetClearUICallback(Action a)
        {
            _clearUICallback = a;
        }

        public void Clear()
        {
            _value = _initialValue;
            _clearUICallback?.Invoke();
        }
    }
}
