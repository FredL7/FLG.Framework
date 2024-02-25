namespace FLG.Cs.IDatamodel {
    public interface ILayout {
        public string Name { get; }
        public ILayoutElement Root { get; }

        void AddObserver(ILayoutObserver observer);
        public ILayoutElement GetTarget(string name);
    }
}
