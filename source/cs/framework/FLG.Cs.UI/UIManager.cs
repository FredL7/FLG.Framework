using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.UI;
using FLG.Cs.Datamodel.UI.Layouts;
using FLG.Cs.Datamodel.UI.Pages;
using FLG.Cs.Math;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;



namespace FLG.Cs.UI {
    public class UIManager(PreferencesUI prefs, ILogManager logger, IUIFactory factory) : IUIManager {
        private readonly string[] _uiDirs = prefs.dirs;
        private Size _windowSize = prefs.windowSize;
        private readonly LayoutsManager _layoutsManager = new();
        private readonly PagesManager _pagesManager = new();

        private readonly List<IUIObserver> _observers = [];

        #region IServiceInstance
        public void OnServiceRegistered()
        {
            logger.Debug("UI Manager Registered");
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
            logger.Debug("Begin XML Parsing");

            XMLParser parser = new(_uiDirs, logger, factory);
            var result = parser.Parse();
            if (!result) logger.Log(result);
            logger.Debug("Finished XML Parsing");

            _layoutsManager.SetLayoutsFromParser(parser.GetLayouts());
            _pagesManager.SetPagesFromParser(parser.GetPages(), this, factory);

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
