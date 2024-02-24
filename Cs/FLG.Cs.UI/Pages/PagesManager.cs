using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;

namespace FLG.Cs.UI.Pages {
    internal class PagesManager {
        Dictionary<string, IPage> _pages;

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
                Locator.Instance.Get<ILogManager>().Error($"Page with id {id} does not exists");
                return;
            }

            _currentPage = id;
        }

        internal IPage GetCurrentPage()
        {
            if (_currentPage == string.Empty)
                Locator.Instance.Get<ILogManager>().Error($"Current page isn't set.");

            return _pages[_currentPage];
        }

        internal string GetCurrentPageLayoutId() => GetCurrentPage().GetLayoutId();

        internal void SetPagesFromParser(Dictionary<string, IPage> pages)
        {
            _pages = pages;
            foreach (IPage page in _pages.Values)
                page.Setup();
        }
    }
}
