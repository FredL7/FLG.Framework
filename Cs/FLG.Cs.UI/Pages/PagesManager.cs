using FLG.Cs.IDatamodel;

namespace FLG.Cs.UI.Pages {
    internal class PagesManager {
        Dictionary<string, IPage> _pages;

        internal PagesManager()
        {
            _pages = new();
        }

        internal void SetPagesFromParser(Dictionary<string, IPage> pages)
        {
            _pages = pages;
            foreach (IPage page in _pages.Values)
                page.Setup();
        }
    }
}
