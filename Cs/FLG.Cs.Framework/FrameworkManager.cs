using FLG.Cs.Decorators;
using FLG.Cs.Datamodel;


namespace FLG.Cs.Framework {
    public class FrameworkManager : SingletonBase<FrameworkManager> {
        private FrameworkManager() { }

        #region General
        private bool _initializedGeneral = false;
        public void InitializeFramework(Preferences pref)
        {
            if (!_initializedGeneral)
            {
                _initializedGeneral = true;
            }
        }

        public void Initialize<T>(T service) where T : IServiceInstance
        {
            ManagersFactory.CreateGeneric(service);
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
                ManagersFactory.CreateLogger(pref, dummy);
                _initializedLogs = true;
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
                ManagersFactory.CreateSerializer(pref);
                _initializedSerializer = true;
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
                ManagersFactory.CreateUIManager(pref);
                _initializedUI = true;
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
                ManagersFactory.CreateNetworkingManager(pref);
                _initializedNetworking = true;
            }
        }

        private bool ValidateDependenciesNetworking() => _initializedGeneral;
        #endregion Networking
    }
}
