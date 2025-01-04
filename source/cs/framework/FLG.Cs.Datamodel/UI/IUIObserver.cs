namespace FLG.Cs.Datamodel.UI {
    public interface IUIObserver {
        public void OnCurrentPageChanged(string pageId, string layoutId);
    }
}
