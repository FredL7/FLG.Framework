namespace FLG.Cs.Datamodel {
    public interface IResult {
        public ELogLevel GetSeverity();
        public string GetMessage();
    }
}
