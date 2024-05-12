﻿namespace FLG.Cs.IDatamodel {
    public class SimpleBoolModel : IInputFieldModel {
        private bool _initialValue;
        private bool _value;

        public SimpleBoolModel(bool initialValue = false) {
            _value = _initialValue = initialValue;
        }

        public bool SetValue(object value)
        {
            var transient = value as bool?;
            if (transient != null)
            {
                _value = transient.Value;
                return true;
            }
            return false;
        }

        public string GetValueAsString() => _value.ToString();
        public int GetValueAsInt() => _value ? 1 : 0;
        public float GetValueAsFloat() => _value ? 1f : 0f;
        public bool GetValueAsBool() => _value;

        public void Clear()
        {
            _value = _initialValue;
        }
    }
}
