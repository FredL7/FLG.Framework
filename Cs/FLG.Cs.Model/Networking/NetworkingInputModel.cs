using System.Net;

using FLG.Cs.Datamodel;


namespace FLG.Cs.Model {
    public class NetworkingInputModel : IInputFieldModel {
        private readonly IPAddress _initialValue;
        private IPAddress _value;
        private Action? _clearUICallback;

        public NetworkingInputModel(string initialValue = "")
        {
            var parseResult = TryParseString(initialValue, out _initialValue);
            _value = _initialValue;
            if (!parseResult)
            {
                // TODO: Log
            }
        }

        private bool TryParseString(string value, out IPAddress result)
        {
            if (IPAddress.TryParse(value, out var res)
                && res.ToString() == value)
            {
                result = res;
                return true;
            }
            else
            {
                result = IPAddress.Loopback;
                return false;
            }
        }

        public bool SetValue(string value)
        {
            return TryParseString(value, out _value);
        }

        public bool SetValue(object value)
        {
            if (value is IPAddress transient)
            {
                _value = transient;
                return true;
            }
            return false;
        }

        public string GetValueAsString() => _value.ToString();
        public int GetValueAsInt() => 0;
        public float GetValueAsFloat() => 0f;
        public bool GetValueAsBool() => false;

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
