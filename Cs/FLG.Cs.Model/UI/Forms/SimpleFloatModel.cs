using FLG.Cs.Datamodel;


namespace FLG.Cs.Model {
    public class SimpleFloatModel : IInputFieldModel {
        private float _initialValue;
        private float _value;
        private Action? _clearUICallback;

        public SimpleFloatModel(float initialValue = 0f)
        {
            _value = _initialValue = initialValue;
        }

        public bool SetValue(string value)
        {
            var transient = Convert.ToString(value);
            if (transient != null)
            {
                if (float.TryParse(transient, out var result))
                {
                    _value = result;
                    return true;
                }
            }
            return false;
        }

        public bool SetValue(object value)
        {
            if (value is float transient)
            {
                _value = transient;
                return true;
            }
            return false;
        }

        public string GetValueAsString() => _value.ToString();
        public int GetValueAsInt() => (int)_value; // Will truncate. If require Round/Ceil/Floor, create custom model
        public float GetValueAsFloat() => _value;
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
