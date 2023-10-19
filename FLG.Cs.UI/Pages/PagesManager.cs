namespace FLG.Cs.UI.Pages {
    internal class PagesManager {
        private Dictionary<uint, AbstractPage> _pages;
        private AbstractPage? _current = null;

        internal PagesManager()
        {
            _pages = new();
        }

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
