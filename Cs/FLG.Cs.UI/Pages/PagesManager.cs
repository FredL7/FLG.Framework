using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Commons;
using FLG.Cs.UI.Layouts;

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
