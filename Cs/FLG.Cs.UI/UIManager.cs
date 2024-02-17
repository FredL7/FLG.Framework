﻿using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;

namespace FLG.Cs.UI {
    public class UIManager : IUIManager {
        private string _layoutsDir;
        private string _pagesDir;

        private LayoutsManager _layoutsManager;
        private PagesManager _pagesManager;

        public UIManager(string layoutsDir, string pagesDir)
        {
            _layoutsDir = layoutsDir;
            _pagesDir = pagesDir;

            _layoutsManager = new();
            _pagesManager = new();
        }

        #region IServiceInstance
        public bool IsProxy() => false;
        public void OnServiceRegisteredFail() { Locator.Instance.Get<ILogManager>().Error("UI Manager Failed to register"); }
        public void OnServiceRegistered() {
            Locator.Instance.Get<ILogManager>().Debug("UI Manager Registered");
            ParseUI();
        }
        #endregion IServiceInstance

        #region IUIManager
        public IEnumerable<ILayout> GetLayouts()
        {
            return _layoutsManager.GetLayouts();
        }
        #endregion

        private void ParseUI()
        {
            var logger = Locator.Instance.Get<ILogManager>();
            logger.Debug("Begin XML Parsing");

            XMLParser parser = new(_layoutsDir, _pagesDir);
            var result = parser.Parse();
            if (!result) result.Log();
            logger.Debug("Finished XML Parsing");

            var fred1 = parser.GetLayouts();
            var fred2 = parser.GetPages();

            _layoutsManager.setLayoutsFromParser(parser.GetLayouts());
            _pagesManager.setPagesFromParser(parser.GetPages());

            Window defaultWindow = new(1920, 1080);
            // TODO: Get actual window size (may via prefs or via watcher below vvvv)
            // TODO: Register window size change to compute on change (also applies to pages)
            _layoutsManager.ComputeLayoutsRectXforms(defaultWindow);
        }
    }
}
