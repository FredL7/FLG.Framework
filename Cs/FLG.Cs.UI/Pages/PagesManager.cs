using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Layouts;

namespace FLG.Cs.UI.Pages {
    internal class PagesManager {
        private Dictionary<string, Page> _pages;
        private Page? _current = null;

        internal PagesManager()
        {
            _pages = new();
        }

        public IEnumerable<Page> GetPages() => _pages.Values;

        internal void RegisterPages(string pagesDir)
        {
            _pages = new();
            var pages = PageXMLParser.Parse(pagesDir);
            if (pages != null)
                foreach (var page in pages)
                {
                    if (!_pages.ContainsKey(page.GetName()))
                    {
                        _pages.Add(page.GetName(), page);
                        Locator.Instance.Get<ILogManager>().Info($"Registered page \"{page.GetName()}\"");
                    }
                    else
                    {
                        Locator.Instance.Get<ILogManager>().Warn($"Already has a page named \"{page.GetName()}\"");
                    }
                }
        }

        internal string GetLayoutIdFromPageId(string id) => _pages[id].LayoutId;

        internal void OpenPage(string id)
        {
            _current?.Close();
            _current = _pages[id];
            _current.Open();
        }
    }
}
