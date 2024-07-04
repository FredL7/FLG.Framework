namespace FLG.Cs.Datamodel {
    public interface ICommandManager : IServiceInstance {
        public void ExecuteCommand(ICommand command);
        public void ExecuteCommand(string commandMessage);
    }
}
