namespace FLG.Cs.IDatamodel {
    public interface IUIManager : IServiceInstance {
        public IEnumerable<ILayout> GetLayouts();
        public ILayout GetLayout(string name);
    }
}
