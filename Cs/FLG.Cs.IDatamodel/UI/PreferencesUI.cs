using FLG.Cs.Math;

namespace FLG.Cs.Datamodel {
    public struct PreferencesUI {
        public string layoutsDir;
        public string pagesDir;
        public Size windowSize;

        public ILogManager logger;
        public IUIFactory factory;
    }
}
