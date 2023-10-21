namespace FLG.Cs.UI.Pages {
    internal class PagesManager {
        private Dictionary<uint, Page> _pages;
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

        internal uint GetLayoutIdFromPageId(uint id) => _pages[id].LayoutId;

        internal void OpenPage(uint id)
        {
            _current?.Close();
            _current = _pages[id];
            _current.Open();
        }
    }
}
