namespace FLG.Cs.Datamodel.Commands {
    public interface ICommand {
        public string ToPacketString();
        public CommandData GetCommandData();
    }
}
