using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Validation;

namespace FLG.Cs.Logger {
    public class LogManager : ILogManager {
        private readonly string _identifier;
        private readonly List<ILogger> _loggers;
        private readonly string _dir;

        private string _networkingId = "";

        public LogManager(PreferencesLogs prefs)
        {
            _identifier = prefs.identifier;
            _dir = prefs.dir;

            // TODO: Support multiple instances of the same logger type?
            _loggers = new(prefs.types.Length);
            foreach (var type in prefs.types)
            {
                AddLogger(type);
            }
        }

        public void AddLogger(ELoggerType type)
        {
            FLGLogger logger = type switch
            {
                ELoggerType.NO_LOGS => new LoggerNoLogs(_identifier, _networkingId),
                ELoggerType.CONSOLE => new LoggerConsole(_identifier, _networkingId),
                ELoggerType.WRITE_FILE => new LoggerWriteFile(_dir, _identifier, _networkingId),
                ELoggerType.NETWORKING => new LoggerNetworking(_identifier, _networkingId),
                ELoggerType.GAME_ENGINE => throw new ArgumentException("Game Engine logger should be added using `Locator.Instance.get<ILogManager>().AddLogger(ILogger)`"),
                ELoggerType.USER => throw new ArgumentException("User logger should be added using `Locator.Instance.get<ILogManager>().AddLogger(ILogger)`"),
                _ => throw new ArgumentException($"Unknown logger type: {type}"),
            };
            _loggers.Add(logger);
        }

        public void SetNetworkingId(string id)
        {
            _networkingId = id;
            foreach (var logger in _loggers)
                logger.SetNetworkingId(id);
        }

        public void AddLogger(ILogger logger)
        {
            _loggers.Add(logger);
        }

        #region IServiceInstance
        public void OnServiceRegistered()
        {
            Debug("Logging Manager Registered");
        }
        #endregion IServiceInstance

        public void Error(string msg, bool external = false)
        {
            foreach (var logger in _loggers)
                logger.Error(msg, external);
            throw new Exception(msg);
        }
        public void Warn(string msg, bool external = false)
        {
            foreach (var logger in _loggers)
                logger.Warn(msg, external);
        }
        public void Info(string msg, bool external = false)
        {
            foreach (var logger in _loggers)
                logger.Info(msg, external);
        }
        public void Debug(string msg, bool external = false)
        {
            foreach (var logger in _loggers)
                logger.Debug(msg, external);
        }

        public void Log(Result result)
        {
            foreach (var logger in _loggers)
                logger.Log(result);
        }
    }
}
