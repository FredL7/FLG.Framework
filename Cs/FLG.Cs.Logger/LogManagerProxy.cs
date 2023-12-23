namespace FLG.Cs.Logger {
    public class LogManagerProxy : ILogManager {
        public bool IsProxy() => true;
        public void OnServiceRegistered() { } // TODO: Log?
        public void OnServiceRegisteredFail() { } // TODO: Log?

        public void Debug(string msg) { }
        public void Error(string msg) { }
        public void Info(string msg) { }
        public void Warn(string msg) { }
    }
}
