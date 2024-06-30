using FLG.Cs.Math;

namespace FLG.Cs.Datamodel {
    public struct PreferencesUI {
        public string[] uiDirs;
        public Size windowSize;

        public ILogManager logger;
        public IUIFactory factory;
    }
}
