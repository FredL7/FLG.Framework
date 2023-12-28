using FLG.Cs.Decorators;
using FLG.Cs.Logger;
using FLG.Cs.Serialization;
using FLG.Cs.UI;

namespace FLG.Cs.Framework {
    public class FrameworkManager : SingletonBase<FrameworkManager> {
        private FrameworkManager() { }

        #region General
        private bool _initializedGeneral = false;
        public void Initialize(Preferences pref)
        {
            if (!_initializedGeneral)
            {
                ManagersFactory.CreateProxies();
                _initializedGeneral = true;
            }
        }
        #endregion General

        #region Logs
        private bool _initializedLogs = false;
        public void InitializeLogs(PreferencesLogs pref)
        {
            if (!ValidateDependenciesLogs())
                return;

            if (!_initializedLogs)
            {
                ManagersFactory.CreateLogger(pref.logsDir);
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
                ManagersFactory.CreateSerializer(pref.serializerType, pref.savesDir);
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
                // TODO: Make sure you have registered pages content before calling this method
                // Locator.Instance.Get<IUIManager>().LoadUI();
                // Observer pattern to call the observers to then draw()
                ManagersFactory.CreateUIManager(pref.layoutsDir);
                _initializedUI = true;
            }
        }

        private bool ValidateDependenciesUI() => _initializedGeneral;
        #endregion UI
    }
}
