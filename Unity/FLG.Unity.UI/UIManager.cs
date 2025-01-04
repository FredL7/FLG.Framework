using FLG.Cs.Datamodel.UI;


namespace FLG.Unity.UI {
    public class UIManager : IUIObserver {
        private string _currentPage = string.Empty;

        public void OnCurrentPageChanged(string pageId, string layoutId)
        {
            if (_currentPage != pageId)
            {
                // TODO
            }
        }
    }
}
