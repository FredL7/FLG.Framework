namespace FLG.Cs.Datamodel {
    public class SimpleIntegerModel : IInputFieldModel {
        private int _initialValue;
        private int _value;
        private Action? _clearUICallback;

        public SimpleIntegerModel(int initialValue = 0)
        {
            _value = _initialValue = initialValue;
        }

        public bool SetValue(string value)
        {
            var transient = Convert.ToString(value);
            if (transient != null)
            {
                if (Int32.TryParse(transient, out var result))
                {
                    _value = result;
                    return true;
                }
            }
            return false;
        }

        public bool SetValue(object value)
        {
            var transient = value as int?;
            if (transient != null)
            {
                _value = transient.Value;
                return true;
            }
            return false;
        }

        public string GetValueAsString() => _value.ToString();
        public int GetValueAsInt() => _value;
        public float GetValueAsFloat() => (float)_value;
        public bool GetValueAsBool() => _value != 0;


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
