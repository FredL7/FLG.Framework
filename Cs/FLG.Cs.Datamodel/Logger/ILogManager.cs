using FLG.Cs.Datamodel.ServiceLocator;
using FLG.Cs.Datamodel.Validation;


namespace FLG.Cs.Datamodel.Logger {
    public interface ILogManager : ILogger, IServiceInstance {
        public void AddLogger(ILogger logger);
        new public void SetNetworkingId(string id);

        new public void Error(string msg, bool external = false);
        new public void Warn(string msg, bool external = false);
        new public void Info(string msg, bool external = false);
        new public void Debug(string msg, bool external = false);
        new public void Log(Result result);
    }
}
