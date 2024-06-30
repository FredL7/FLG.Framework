using FLG.Cs.Datamodel;


namespace FLG.Cs.Model {
    public class SimpleStringModel : IInputFieldModel {
        private string _initialValue;
        private string _value;
        private Action? _resetUICallback;

        public SimpleStringModel(string initialValue = "")
        {
            _value = _initialValue = initialValue;
        }

        public bool SetValue(string value)
        {
            _value = value;
            return true;
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


        public void SetResetCallback(Action a)
        {
            _resetUICallback = a;
        }

        public void Reset()
        {
            _value = _initialValue;
            _resetUICallback?.Invoke();
        }
    }
}
