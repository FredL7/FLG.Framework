﻿using FLG.Cs.Datamodel;


namespace FLG.Cs.Logger {
    public class LogManagerDummy : ILogManager {

        public LogManagerDummy() { }

        #region IServiceInstance
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered()
        {
            Debug("Log Manager Dummy Registered");
        }
        #endregion IServiceInstance

        public void Error(string msg) { }
        public void Warn(string msg) {  }
        public void Info(string msg) {  }
        public void Debug(string msg) {  }

        public void Log(IResult result) { }
    }
}
