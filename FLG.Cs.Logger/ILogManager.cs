using FLG.Cs.ServiceLocator;

namespace FLG.Cs.Logger {
    public interface ILogManager : IServiceInstance {
        public void Error(Type source, string method, string msg);
        public void Warn(Type source, string method, string msg);
        public void Info(Type source, string method, string msg);
        public void Debug(Type source, string method, string msg);
    }
}
