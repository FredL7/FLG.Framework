using FLG.Cs.Datamodel.ServiceLocator;


namespace FLG.Cs.Datamodel.Commands {
    public interface ICommandManager : IServiceInstance {
        public void ExecuteCommand(ICommand command);
        public void ExecuteCommand(string commandMessage);
    }
}
