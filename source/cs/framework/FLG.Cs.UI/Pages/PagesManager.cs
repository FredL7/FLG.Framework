using System.Collections.Generic;

using FLG.Cs.Datamodel.UI.Pages;
using FLG.Cs.Datamodel.UI;


namespace FLG.Cs.UI.Pages {
    internal class PagesManager {
        private Dictionary<string, IPage> _pages;

        private string _currentPage;

        internal PagesManager()
        {
            _pages = [];
            _currentPage = string.Empty;
        }

        internal IPage GetPage(string id) => _pages[id];

        internal void SetCurrentPage(string id)
        {
            if (!_pages.TryGetValue(id, out IPage? value))
            {
                throw new Exception($"Page with id {id} does not exists");
            }

            value.OnClose();

            _currentPage = id;
            value.OnOpen();
        }

        internal IPage GetCurrent()
        {
            if (_currentPage == string.Empty)
                throw new Exception($"Current page isn't set.");

            return _pages[_currentPage];
        }

        internal void SetPagesFromParser(Dictionary<string, IPage> pages, IUIManager ui, IUIFactory factory)
        {
            _pages = pages;
            foreach (IPage page in _pages.Values)
                page.Setup(ui, factory);
        }

        internal void RegisterPages()
        {
            foreach (IPage page in _pages.Values)
                page.OnRegister();
        }
    }
}
