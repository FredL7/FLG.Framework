using FLG.Cs.Datamodel.Validation;


namespace FLG.Cs.Datamodel.Logger {
    public interface ILogger {
        public void SetNetworkingId(string id);

        public void Error(string msg, bool external = false);
        public void Warn(string msg, bool external = false);
        public void Info(string msg, bool external = false);
        public void Debug(string msg, bool external = false);

        public void Log(Result result);
    }
}
