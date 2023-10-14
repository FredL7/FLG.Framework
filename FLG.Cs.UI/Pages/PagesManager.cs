namespace FLG.Cs.UI.Pages {
    internal class PagesManager {
        private Dictionary<uint, AbstractPage> _pages;
        private AbstractPage? _current = null;

        internal PagesManager()
        {
            _pages = new();
        }

        internal void RegisterPage(uint id, AbstractPage page)
        {
            _pages.Add(id, page);
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
