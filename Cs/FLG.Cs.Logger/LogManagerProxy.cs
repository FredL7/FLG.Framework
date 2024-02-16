using FLG.Cs.IDatamodel;

namespace FLG.Cs.Logger {
    public class LogManagerProxy : ILogManager {
        public bool IsProxy() => true;
        public void OnServiceRegistered() { }
        public void OnServiceRegisteredFail() { }

        public void Debug(string msg) { }
        public void Error(string msg) { }
        public void Info(string msg) { }
        public void Warn(string msg) { }
    }
}
