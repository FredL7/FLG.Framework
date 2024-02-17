namespace FLG.Cs.IDatamodel {
    public interface IUIManager : IServiceInstance {
        public IEnumerable<ILayout> GetLayouts();
    }
}
