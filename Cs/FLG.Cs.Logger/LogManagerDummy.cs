using FLG.Cs.IDatamodel;


namespace FLG.Cs.Logger {
    public class LogManagerDummy : ILogManager {

        public LogManagerDummy() { }

        #region IServiceInstance
        public bool IsProxy() => false;
        public void OnServiceRegisteredFail() { Error("Log Manager Dummy Failed to register"); }
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
