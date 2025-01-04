using FLG.Cs.Datamodel.ServiceLocator;
using FLG.Cs.Datamodel.Validation;


namespace FLG.Cs.Datamodel.Logger {
    public interface ILogManager : ILogger, IServiceInstance {
        public void AddLogger(ILogger logger);
        new public void SetNetworkingId(string id);

        new public void Error(string msg);
        new public void Warn(string msg);
        new public void Info(string msg);
        new public void Debug(string msg);
        new public void Log(Result result);
        new public void LogEntry(LogEntry logEntry);
    }
}
