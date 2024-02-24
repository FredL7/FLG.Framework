namespace FLG.Cs.IDatamodel {
    public interface IUIObserver {
        public void OnCurrentPageChanged(string pageId, string layoutId);
    }
}
