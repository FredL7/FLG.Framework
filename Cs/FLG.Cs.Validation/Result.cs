using FLG.Cs.Datamodel;


namespace FLG.Cs.Validation {
    public readonly struct Result : IResult {
        public readonly static Result SUCCESS = new("SUCCESS", true, ELogLevel.DEBUG);

        public readonly string message;
        public readonly bool success;
        private readonly ELogLevel severity;

        public Result(string message, bool success = false, ELogLevel severity = ELogLevel.ERROR)
        {
            this.success = success;
            this.message = message;
            this.severity = severity;
        }

        public static implicit operator bool(Result r) => r.success;

        public ELogLevel GetSeverity() => severity;
        public string GetMessage() => message;
    }
}
