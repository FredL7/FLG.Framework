using Godot;

using FLG.Cs.Datamodel.Framework;
using FLG.Cs.Datamodel.UI;

using FLG.Cs.Framework;


namespace FLG.Godot.UI {
    public class UIManager : IUIObserver {
        private readonly UIPainter _painter;
        private readonly UITool _tools;

        private string _currentLayout = string.Empty;
        private string _currentPage = string.Empty;

        public UIManager(PreferencesFramework prefs, Node node, bool fromEditor)
        {
            if (prefs.logs == null)
            {
                throw new Exception("Sanitized preferences (logs) should not be null");
            }

            PreferencesFrameworkSanitized sanitizedPrefs = FrameworkManager.SanitizePreferences(prefs);
            _painter = new(node);
            _tools = new(fromEditor, sanitizedPrefs.logs, sanitizedPrefs.ui);

            if (!fromEditor)
            {
                _tools.UI.AddObserver(this);
            }

            Setup(sanitizedPrefs.ui.homepage);
        }

        private void Setup(string homepage)
        {
            _painter.Clear();
            _painter.Draw(_tools);
            _tools.UI.SetCurrentPage(homepage);
        }

        public void OnCurrentPageChanged(string pageId, string layoutId)
        {
            if (_currentPage != pageId)
            {
                _painter.ChangePage(pageId, layoutId, _currentLayout);
                _currentPage = pageId;
                _currentLayout = layoutId;
            }
        }
    }
}
