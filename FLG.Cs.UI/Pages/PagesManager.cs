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
            // _pages.Add(id, page);
            Console.WriteLine(pagesDir);
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
