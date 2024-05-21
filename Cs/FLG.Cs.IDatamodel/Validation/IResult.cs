namespace FLG.Cs.IDatamodel {
    public interface IResult {
        public ELogLevel GetSeverity();
        public string GetMessage();
    }
}
