using FLG.Cs.Datamodel;


namespace FLG.Cs.Logger {
    public class LogManagerNoLogs : LogManager {

        public LogManagerNoLogs(PreferencesLogs prefs) : base(prefs) { }

        #region IServiceInstance
        public override void OnServiceRegisteredFail() { }
        public override void OnServiceRegistered()
        {
            Debug("Log Manager (No Logs) Registered");
        }
        #endregion IServiceInstance

        protected override void Log(string msg, ELogLevel serverity) { }
    }
}
