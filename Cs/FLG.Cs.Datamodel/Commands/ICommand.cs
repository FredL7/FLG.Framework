namespace FLG.Cs.Datamodel.Commands {
    public interface ICommand {
        public string ToMessageString();
        public CommandData GetCommandData();
    }
}
