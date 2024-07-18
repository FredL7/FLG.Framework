namespace FLG.Cs.Datamodel {
    public interface ILogManager : ILogger, IServiceInstance {
        public void AddLogger(ILogger logger);
    }
}
