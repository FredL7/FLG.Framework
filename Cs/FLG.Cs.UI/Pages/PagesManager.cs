using FLG.Cs.IDatamodel;

namespace FLG.Cs.UI.Pages {
    internal class PagesManager {
        Dictionary<string, IPage> _pages;

        internal PagesManager()
        {
            _pages = new();
        }

        internal void setPagesFromParser(Dictionary<string, IPage> pages)
        {
            _pages = pages;
        }
    }
}
