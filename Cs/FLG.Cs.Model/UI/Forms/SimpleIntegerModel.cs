using FLG.Cs.Datamodel;


namespace FLG.Cs.Model {
    public class SimpleIntegerModel : IInputFieldModel {
        private int _initialValue;
        private int _value;
        private Action? _resetUICallback;

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
            if (value is int transient)
            {
                _value = transient;
                return true;
            }
            return false;
        }

        public string GetValueAsString() => _value.ToString();
        public int GetValueAsInt() => _value;
        public float GetValueAsFloat() => (float)_value;
        public bool GetValueAsBool() => _value != 0;


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
