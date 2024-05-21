﻿namespace FLG.Cs.Datamodel {
    public interface IInputFieldModel {
        public bool SetValue(object value);

        public string GetValueAsString();
        public int GetValueAsInt();
        public float GetValueAsFloat();
        public bool GetValueAsBool();

        public void SetClearUICallback(Action a);
        public void Clear();
    }
}
