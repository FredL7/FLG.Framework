using FLG.Cs.Decorators;
using FLG.Cs.Datamodel;
using FLG.Cs.Model;


namespace FLG.Cs.Framework {
    public class FrameworkManager : SingletonBase<FrameworkManager> {
        private readonly List<IGameLoopObject> _gameLoopObjects;

        private FrameworkManager()
        {
            _gameLoopObjects = new();
        }

        #region Initializer

        #region General
        private bool _initializedFramework = false;
        public Result InitializeFramework(Preferences pref)
        {
            if (!_initializedFramework)
            {
                _initializedFramework = true;
                return Result.SUCCESS;
            }

            return new Result("Could not initialize Framework: Already initialized", severity: ELogLevel.WARN);
        }
        #endregion General

        #region Logs
        private bool _initializedLogs = false;
        public Result InitializeLogs(PreferencesLogs pref)
        {
            if (!ValidateDependenciesLogs())
                return new Result($"Could not initialize Log Manager: dependencies not initialized (Framework={_initializedFramework}");

            if (!_initializedLogs)
            {
                var result = ManagersFactory.CreateLogger(pref);
                if (result.result)
                {
                    _initializedLogs = true;
                    return Result.SUCCESS;
                }
                else
                {
                    return result.result;
                }
            }

            return new Result("Could not initialize Log Manager: Already initialized", severity: ELogLevel.WARN);
        }

        private bool ValidateDependenciesLogs() => _initializedFramework;
        #endregion Logs

        #region Serialization
        private bool _initializedSerializer = false;
        public Result InitializeSerializer(PreferencesSerialization pref)
        {
            if (!ValidateDependenciesSerialization())
                return new Result($"Could not initialize Serializer Manager: dependencies not initialized (Framework={_initializedFramework}");

            if (!_initializedSerializer)
            {
                var result = ManagersFactory.CreateSerializer(pref);
                if (result.result)
                {
                    _initializedSerializer = true;
                    return Result.SUCCESS;
                }
                else
                {
                    return result.result;
                }
            }

            return new Result("Could not initialize Serializer Manager: Already initialized", severity: ELogLevel.WARN);
        }

        private bool ValidateDependenciesSerialization() => _initializedFramework;
        #endregion Serialization

        #region UI
        private bool _initializedUI = false;
        public Result InitializeUI(PreferencesUI pref)
        {
            if (!ValidateDependenciesUI())
                return new Result($"Could not initialize UI Manager: dependencies not initialized (Framework={_initializedFramework}");

            if (!_initializedUI)
            {
                var result = ManagersFactory.CreateUIManager(pref);
                if (result.result)
                {
                    _initializedUI = true;
                    return Result.SUCCESS;
                }
                else
                {
                    return result.result;
                }
            }

            return new Result("Could not initialize UI Manager: Already initialized", severity: ELogLevel.WARN);
        }

        private bool ValidateDependenciesUI() => _initializedFramework;
        #endregion UI

        #region Networking
        private bool _initializedNetworking = false;
        public Result InitializeNetworking(PreferencesNetworking pref)
        {
            if (!ValidateDependenciesNetworking())
                return new Result($"Could not initialize Networking Manager: dependencies not initialized (Framework={_initializedFramework}");

            if (!_initializedNetworking)
            {
                var networkingResult = ManagersFactory.CreateNetworkingManager(pref);
                if (!networkingResult.result)
                    return networkingResult.result;

                var commandResult = ManagersFactory.CreateCommandManager();
                if (!commandResult.result)
                    return commandResult.result;

                if (networkingResult.manager != null && commandResult.manager != null)
                {
                    _initializedNetworking = true;
                    _gameLoopObjects.Add(networkingResult.manager);
                    return Result.SUCCESS;
                }
                else
                {
                    return new Result("Could not initialize Networking Manager: manager initialization failure");
                }
            }

            return new Result("Could not initialize Networking Manager: Already initialized", severity: ELogLevel.WARN);
        }

        private bool ValidateDependenciesNetworking() => _initializedFramework;
        #endregion Networking

        #endregion Initializer

        public void Update()
        {
            foreach (var manager in _gameLoopObjects)
            {
                manager.Update();
            }
        }
    }
}
