using FLG.Cs.Datamodel.ServiceLocator;


namespace FLG.Cs.Datamodel.Logger {
    public interface ILogManager : ILogger, IServiceInstance {
        public void AddLogger(ILogger logger);
    }
}
