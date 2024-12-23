using FLG.Cs.Datamodel.Validation;


namespace FLG.Cs.Datamodel.Logger {
    public interface ILogger {
        public void Error(string msg);
        public void Warn(string msg);
        public void Info(string msg);
        public void Debug(string msg);

        public void Log(Result result);
    }
}
