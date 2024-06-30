namespace FLG.Cs.Datamodel {
    public interface IUIManager : IServiceInstance {
        public void SetCurrentPage(string id);
        public IEnumerable<ILayout> GetLayouts();
        public ILayout GetLayout(string name);
        public IPage GetPage(string id);
        public void AddObserver(IUIObserver observer);
        public void RemoveObserver(IUIObserver observer);
    }
}
