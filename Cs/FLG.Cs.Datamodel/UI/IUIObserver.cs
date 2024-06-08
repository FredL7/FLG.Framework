namespace FLG.Cs.Datamodel {
    public interface IUIObserver {
        public void OnCurrentPageChanged(string pageId, string layoutId);
    }
}
