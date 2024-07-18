using FLG.Cs.Decorators;
using FLG.Cs.Datamodel;
using FLG.Cs.Model;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Framework {
    public class FrameworkManager : SingletonBase<FrameworkManager> {
        private readonly List<IGameLoopObject> _gameLoopObjects;

        private FrameworkManager()
        {
            _gameLoopObjects = new();
        }

        #region Initializer
        public Result Initialize(PreferencesFramework prefs)
        {
            Result result = InitializeFramework();
            if (!result) return result;

            result = InitializeLogs(prefs.logs);
            if (!result) return result;

            result = InitializeUI(prefs.ui);
            if (!result) return result;

            result = InitializeSerializer(prefs.serialization);
            if (!result) return result;

            result = InitializeNetworking(prefs.networking);
            if (!result) return result;

            return Result.SUCCESS;
        }

        #region General
        private bool _initializedFramework = false;
        private Result InitializeFramework()
        {
            // Only to prevent more than one initialization
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

        private Result InitializeLogs(PreferencesLogs? pref)
        {
            if (pref != null)
            {
                PreferencesLogs prefNoNull = pref.Value;
                var result = InitializeLogsInner(prefNoNull);
                if (!result) return result;
                Locator.Instance.Get<ILogManager>().Info("Initialized Log Manager with user prefs");
            }
            else
            {
                PreferencesLogs prefOverride = new()
                {
                    types = new[] { ELoggerType.NO_LOGS },
                };
                var result = InitializeLogsInner(prefOverride);
                if (!result) return result;
                Locator.Instance.Get<ILogManager>().Debug("Initialized Log Manager with default prefs (no logs)");
            }

            return Result.SUCCESS;
        }

        private Result InitializeLogsInner(PreferencesLogs pref)
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

        private bool ValidateDependenciesLogs() => _initializedFramework && _initializedLogs;
        #endregion Logs

        #region Serialization
        private bool _initializedSerializer = false;

        private Result InitializeSerializer(PreferencesSerialization? pref)
        {
            if (pref != null)
            {
                PreferencesSerialization prefNoNull = pref.Value;
                var result = InitializeSerializerInner(prefNoNull);
                if (!result) return result;
                Locator.Instance.Get<ILogManager>().Info("Initialized Serialization Manager with user prefs");
            }
            return Result.SUCCESS;
        }

        private Result InitializeSerializerInner(PreferencesSerialization pref)
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

        private bool ValidateDependenciesSerialization() => _initializedFramework && _initializedLogs;
        #endregion Serialization

        #region UI
        private bool _initializedUI = false;

        private Result InitializeUI(PreferencesUI? pref)
        {
            if (pref != null)
            {
                PreferencesUI prefNoNull = pref.Value;
                var result = InitializeUIInner(prefNoNull);
                if (!result) return result;
                Locator.Instance.Get<ILogManager>().Info("Initialized UI Manager with user prefs");
            }
            else
            {
                Locator.Instance.Get<ILogManager>().Debug("UI Manager Not initialized");
            }

            return Result.SUCCESS;
        }

        private Result InitializeUIInner(PreferencesUI pref)
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

        private bool ValidateDependenciesUI() => _initializedFramework && _initializedLogs;
        #endregion UI

        #region Networking
        private bool _initializedNetworking = false;

        private Result InitializeNetworking(PreferencesNetworking? pref)
        {
            if (pref != null)
            {
                PreferencesNetworking prefNoNull = pref.Value;
                var result = InitializeNetworkingInner(prefNoNull);
                if (!result) return result;
                Locator.Instance.Get<ILogManager>().Info("Initialized Networking Manager with user prefs");
            }
            else
            {
                PreferencesNetworking prefOverride = new()
                {
                    clientType = ENetworkClientType.OFFLINE,
                };
                var result = InitializeNetworkingInner(prefOverride);
                if (!result) return result;
                Locator.Instance.Get<ILogManager>().Debug("Initialized networking Manager with default prefs (offline)");
            }

            return Result.SUCCESS;
        }

        private Result InitializeNetworkingInner(PreferencesNetworking pref)
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

        private bool ValidateDependenciesNetworking() => _initializedFramework && _initializedLogs;
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
