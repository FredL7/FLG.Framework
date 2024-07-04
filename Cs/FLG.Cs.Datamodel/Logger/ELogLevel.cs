namespace FLG.Cs.Datamodel {
    public enum ELogLevel {
        ERROR = 0,
        WARN = 1,
        INFO = 2,
        DEBUG = 3
    }

    public static class ELogLevelExtension {
        public static string ToPrettyString(this ELogLevel level)
        {
            return level switch
            {
                ELogLevel.ERROR => "ERROR",
                ELogLevel.WARN => "WARNING",
                ELogLevel.INFO => "INFO",
                ELogLevel.DEBUG => "DEBUG",
                _ => throw new ArgumentException($"{level} is not valid"),
            };
        }
    }
}
