using FLG.Cs.Datamodel.Logger;


namespace FLG.Cs.Datamodel.Validation {
    public readonly struct Result(string message, bool success = false, ELogLevel severity = ELogLevel.ERROR) {
        public readonly static Result SUCCESS = new("SUCCESS", true, ELogLevel.DEBUG);

        private readonly string message = message;
        private readonly bool success = success;
        private readonly ELogLevel severity = severity;

        public static implicit operator bool(Result r) => r.success;

        public ELogLevel GetSeverity() => severity;
        public string GetMessage() => message;
    }
}
