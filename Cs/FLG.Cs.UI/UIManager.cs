using FLG.Cs.Datamodel;
using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;

namespace FLG.Cs.UI {
    public class UIManager : IUIManager {
        private readonly string[] _uiDirs;
        private Size _windowSize;

        private readonly ILogManager _logger;
        private readonly IUIFactory _factory;

        private readonly LayoutsManager _layoutsManager;
        private readonly PagesManager _pagesManager;

        private readonly List<IUIObserver> _observers;

        public UIManager(PreferencesUI prefs, ILogManager logger, IUIFactory factory)
        {
            _uiDirs = prefs.dirs;
            _windowSize = prefs.windowSize;

            _logger = logger;
            _factory = factory;

            _layoutsManager = new();
            _pagesManager = new();

            _observers = new();
        }

        #region IServiceInstance
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered()
        {
            _logger.Debug("UI Manager Registered");
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
        public IPage GetPage(string id) => _pagesManager.GetPage(id);

        public void SetupUI()
        {
            ParseUI();
            _pagesManager.RegisterPages();
        }
        #endregion

        private void ParseUI()
        {
            _logger.Debug("Begin XML Parsing");

            XMLParser parser = new(_uiDirs, _logger, _factory);
            var result = parser.Parse();
            if (!result) _logger.Log(result);
            _logger.Debug("Finished XML Parsing");

            _layoutsManager.SetLayoutsFromParser(parser.GetLayouts());
            _pagesManager.SetPagesFromParser(parser.GetPages(), this, _factory);

            // TODO: Register window size change to compute on change (also applies to pages)
            _layoutsManager.ComputeLayoutsRectXforms(_windowSize);
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
