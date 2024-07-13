using FLG.Cs.Decorators;
using FLG.Cs.Datamodel;


namespace FLG.Cs.Framework {
    public class FrameworkManager : SingletonBase<FrameworkManager> {
        private List<IGameLoopObject> _gameLoopObjects;
        private FrameworkManager()
        {
            _gameLoopObjects = new(1);
        }

        #region Initializer

        #region General
        private bool _initializedGeneral = false;
        public void InitializeFramework(Preferences pref)
        {
            if (!_initializedGeneral)
            {
                _initializedGeneral = true;
            }
        }
        #endregion General

        #region Logs
        private bool _initializedLogs = false;
        public void InitializeLogs(PreferencesLogs pref, bool dummy = false)
        {
            if (!ValidateDependenciesLogs())
                return;

            if (!_initializedLogs)
            {
                var result = ManagersFactory.CreateLogger(pref, dummy);
                if (result.result)
                {
                    _initializedLogs = true;
                }
            }
        }

        private bool ValidateDependenciesLogs() => _initializedGeneral;
        #endregion Logs

        #region Serialization
        private bool _initializedSerializer = false;
        public void InitializeSerializer(PreferencesSerialization pref)
        {
            if (!ValidateDependenciesSerialization())
                return;

            if (!_initializedSerializer)
            {
                var result = ManagersFactory.CreateSerializer(pref);
                if (result.result)
                {
                    _initializedSerializer = true;
                }
            }
        }

        private bool ValidateDependenciesSerialization() => _initializedGeneral;
        #endregion Serialization

        #region UI
        private bool _initializedUI = false;
        public void InitializeUI(PreferencesUI pref)
        {
            if (!ValidateDependenciesUI())
                return;

            if (!_initializedUI)
            {
                var result = ManagersFactory.CreateUIManager(pref);
                if (result.result)
                {
                    _initializedUI = true;
                }
            }
        }

        private bool ValidateDependenciesUI() => _initializedGeneral;
        #endregion UI

        #region Networking
        private bool _initializedNetworking = false;
        public void InitializeNetworking(PreferencesNetworking pref)
        {
            if (!ValidateDependenciesNetworking())
                return;

            var networkingResult = ManagersFactory.CreateNetworkingManager(pref);
            var commandResult = ManagersFactory.CreateCommandManager();
            if (networkingResult.result && commandResult.result)
            {
                _initializedNetworking = true;
                if (networkingResult.manager != null && commandResult.manager != null)
                {
                    _gameLoopObjects.Add(networkingResult.manager);
                }
            }
        }

        private bool ValidateDependenciesNetworking() => _initializedGeneral && !_initializedNetworking;
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
