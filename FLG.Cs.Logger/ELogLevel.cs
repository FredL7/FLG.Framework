namespace FLG.Cs.Logger {
    public enum ELogLevel {
        ERROR = 0,
        WARN = 1,
        INFO = 2,
        DEBUG = 3
    }

    internal static class ELogLevelExtension
    {
        public static string ToPrettyString(this ELogLevel level)
        {
            return level switch
            {
                ELogLevel.ERROR => "Error",
                ELogLevel.WARN => "Warning",
                ELogLevel.INFO => "Info",
                ELogLevel.DEBUG => "debug",
                _ => throw new NotImplementedException()
            };
        }
    }
}
