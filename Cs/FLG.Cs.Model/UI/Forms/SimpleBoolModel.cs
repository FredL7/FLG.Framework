using FLG.Cs.Datamodel;


namespace FLG.Cs.Model {
    public class SimpleBoolModel : IInputFieldModel {
        private bool _initialValue;
        private bool _value;
        private Action? _resetUICallback;

        public SimpleBoolModel(bool initialValue = false) {
            _value = _initialValue = initialValue;
        }

        public bool SetValue(string value)
        {
            var transient = Convert.ToString(value);
            if (transient != null)
            {
                if (Boolean.TryParse(transient, out var result))
                {
                    _value = result;
                    return true;
                }
            }
            return false;
        }

        public bool SetValue(object value)
        {
            if (value is bool transient)
            {
                _value = transient;
                return true;
            }
            return false;
        }

        public string GetValueAsString() => _value.ToString();
        public int GetValueAsInt() => _value ? 1 : 0;
        public float GetValueAsFloat() => _value ? 1f : 0f;
        public bool GetValueAsBool() => _value;


        public  void SetResetCallback(Action a)
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
