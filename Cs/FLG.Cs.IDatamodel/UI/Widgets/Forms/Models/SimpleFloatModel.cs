namespace FLG.Cs.Datamodel {
    public class SimpleFloatModel : IInputFieldModel {
        private float _initialValue;
        private float _value;
        private Action? _clearUICallback;

        public SimpleFloatModel(float initialValue = 0f)
        {
            _value = _initialValue = initialValue;
        }

        public bool SetValue(object value)
        {
            var transient = value as float?;
            if (transient != null)
            {
                _value = transient.Value;
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
