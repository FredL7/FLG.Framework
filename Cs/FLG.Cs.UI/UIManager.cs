using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;

namespace FLG.Cs.UI {
    public class UIManager : IUIManager {
        private string _layoutsDir;
        private string _pagesDir;

        private LayoutsManager _layoutsManager;
        private PagesManager _pagesManager;

        List<IUIObserver> _observers;

        public UIManager(string layoutsDir, string pagesDir)
        {
            _layoutsDir = layoutsDir;
            _pagesDir = pagesDir;

            _layoutsManager = new();
            _pagesManager = new();

            _observers = new();
        }

        #region IServiceInstance
        public bool IsProxy() => false;
        public void OnServiceRegisteredFail() { Locator.Instance.Get<ILogManager>().Error("UI Manager Failed to register"); }
        public void OnServiceRegistered()
        {
            Locator.Instance.Get<ILogManager>().Debug("UI Manager Registered");
            ParseUI();
        }
        #endregion IServiceInstance

        #region IUIManager
        public void SetCurrentPage(string id)
        {
            _pagesManager.SetCurrentPage(id);
            string layoutId = _pagesManager.GetCurrent().LayoutId;
            _layoutsManager.SetCurrentLayout(layoutId);

            NotifyObservers();
        }

        public void AddObserver(IUIObserver observer) { _observers.Add(observer); }
        public void RemoveObserver(IUIObserver observer)
        {
            if (_observers.Contains(observer))
                _observers.Remove(observer);
        }

        public IEnumerable<ILayout> GetLayouts() => _layoutsManager.GetLayouts();
        public ILayout GetLayout(string name) => _layoutsManager.GetLayout(name);
        #endregion

        private void ParseUI()
        {
            var logger = Locator.Instance.Get<ILogManager>();
            logger.Debug("Begin XML Parsing");

            XMLParser parser = new(_layoutsDir, _pagesDir);
            var result = parser.Parse();
            if (!result) result.Log();
            logger.Debug("Finished XML Parsing");

            _layoutsManager.SetLayoutsFromParser(parser.GetLayouts());
            _pagesManager.SetPagesFromParser(parser.GetPages());

            Window defaultWindow = new(1920, 1080);
            // TODO: Get actual window size (may via prefs or via watcher below vvvv)
            // TODO: Register window size change to compute on change (also applies to pages)
            _layoutsManager.ComputeLayoutsRectXforms(defaultWindow);
        }

        private void NotifyObservers()
        {
            string pageId = _pagesManager.GetCurrent().PageId;
            string layoutId = _layoutsManager.GetCurrent().Name;

            foreach (IUIObserver o in _observers)
                o.OnCurrentPageChanged(pageId, layoutId);
        }
    }
}
