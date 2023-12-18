using FLG.Cs.Decorators;
using FLG.Cs.Factory;
using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI;

namespace FLG.Cs.Framework {
    public class FrameworkManager : SingletonBase<FrameworkManager> {
        private bool _initialized = false;
        private FrameworkManager() { }

        public void Initialize(Preferences p)
        {
            if (!_initialized)
            {
                _initialized = true;
                LogManager.Instance.Initialize(p.logsDir);
                ManagerFactory.CreateSerializer(p.serializerType, p.savesDir);
                ManagerFactory.CreateUIManager(p.layoutsDir, p.pagesDir);
            }
        }

        public void BootstrapUI()
        {
            // TODO: Make sure you have registered pages content before calling this method
            // Locator.Instance.Get<IUIManager>().LoadUI();
            // Observer pattenr to call the observers to then draw()
        }
    }
}
