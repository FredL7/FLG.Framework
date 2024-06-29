using FLG.Cs.Decorators;
using FLG.Cs.Datamodel;


namespace FLG.Cs.Framework {
    public class FrameworkManager : SingletonBase<FrameworkManager> {
        private List<IGameLoopObject> _gameLoopObjects;
        private FrameworkManager()
        {
            _gameLoopObjects = new(1);
        }

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
                var manager = ManagersFactory.CreateLogger(pref, dummy);
                _initializedLogs = manager != null;
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
                var manager = ManagersFactory.CreateSerializer(pref);
                _initializedSerializer = manager != null;
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
                var manager = ManagersFactory.CreateUIManager(pref);
                _initializedUI = manager != null;
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

            if (!_initializedNetworking)
            {
                var manager = ManagersFactory.CreateNetworkingManager(pref);
                _initializedNetworking = manager != null;

                if (manager != null)
                {
                    _gameLoopObjects.Add(manager);
                }
            }
        }

        private bool ValidateDependenciesNetworking() => _initializedGeneral;
        #endregion Networking

        public void Update()
        {
            foreach (var manager in _gameLoopObjects)
            {
                manager.Update();
            }
        }
    }
}
