using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Logger {
    public interface ILogManager : IServiceInstance {
        public void Error(string msg);
        public void Warn(string msg);
        public void Info(string msg);
        public void Debug(string msg);
    }
}
