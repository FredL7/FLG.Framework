using FLG.Cs.IDatamodel;

namespace FLG.Cs.UI.Pages {
    internal class PagesManager {
        private Dictionary<string, IPage> _pages;

        private string _currentPage;

        internal PagesManager()
        {
            _pages = new();
            _currentPage = string.Empty;
        }

        internal void SetCurrentPage(string id)
        {
            if (!_pages.ContainsKey(id))
            {
                throw new Exception($"Page with id {id} does not exists");
            }

            _pages[id].OnClose();

            _currentPage = id;
            _pages[id].OnOpen();
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
    }
}
