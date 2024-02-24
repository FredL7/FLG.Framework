namespace FLG.Cs.IDatamodel {
    public interface IUIManager : IServiceInstance {
        public void SetCurrentPage(string id);
        public IEnumerable<ILayout> GetLayouts();
        public ILayout GetLayout(string name);
        public void AddObserver(IUIObserver observer);
    }
}
