using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Validation {
    public readonly struct Result {
        // TODO: Default to debug or info?
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

        public readonly void Log()
        {
            switch(severity)
            {
                case ELogLevel.INFO:
                    Locator.Instance.Get<ILogManager>().Info(message);
                    break;
                case ELogLevel.DEBUG:
                    Locator.Instance.Get<ILogManager>().Debug(message);
                    break;
                case ELogLevel.WARN:
                    Locator.Instance.Get<ILogManager>().Warn(message);
                    break;
                case ELogLevel.ERROR:
                    Locator.Instance.Get<ILogManager>().Error(message);
                    break;
            }
        }
    }
}
