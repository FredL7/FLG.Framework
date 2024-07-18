using FLG.Cs.Datamodel;


namespace FLG.Cs.Logger {
    public class LogManager : ILogManager {
        private readonly List<ILogger> _loggers;
        private readonly string dir;

        public LogManager(PreferencesLogs prefs)
        {
            dir = prefs.dir;

            _loggers = new(prefs.types.Length);
            foreach (var type in prefs.types)
            {
                AddLogger(type);
            }
        }

        public void AddLogger(ELoggerType type)
        {
            Logger logger = type switch
            {
                ELoggerType.NO_LOGS => new LoggerNoLogs(),
                ELoggerType.WRITE_FILE => new LoggerWriteFile(dir),
                ELoggerType.NETWORKING => new LoggerNetworking(),
                ELoggerType.GAME_ENGINE => throw new ArgumentException("Game Engine logger should be added using `Locator.Instance.get<ILogManager>().AddLogger(ILogger)`"),
                _ => throw new ArgumentException($"Unknown logger type: {type}"),
            };
            _loggers.Add(logger);
        }

        public void AddLogger(ILogger logger)
        {
            _loggers.Add(logger);
        }

        #region IServiceInstance
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered()
        {
            Debug("Logging Manager Registered");
        }
        #endregion IServiceInstance

        public void Error(string msg)
        {
            foreach (var logger in _loggers)
                logger.Error(msg);
            throw new Exception(msg);
        }
        public void Warn(string msg)
        {
            foreach (var logger in _loggers)
                logger.Warn(msg);
        }
        public void Info(string msg)
        {
            foreach (var logger in _loggers)
                logger.Info(msg);
        }
        public void Debug(string msg)
        {
            foreach (var logger in _loggers)
                logger.Debug(msg);
        }

        public void Log(IResult result) {
            foreach (var logger in _loggers)
                logger.Log(result);
        }
    }
}
