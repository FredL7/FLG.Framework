using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.UI;

using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;


namespace FLG.Godot.UI {
    internal class UITool {
        public ILogManager Logger { get; private set; }
        public IUIManager UI { get; private set; }

        public UITool(bool fromEditor, PreferencesLogs prefsLog, PreferencesUI prefsUI)
        {
            if (fromEditor)
            {
                Logger = new LogManager(prefsLog);
                UI = new FLG.Cs.UI.UIManager(prefsUI, Logger);
            }
            else
            {
                Logger = Locator.Instance.Get<ILogManager>();
                UI = Locator.Instance.Get<IUIManager>();
            }
        }
    }
}
