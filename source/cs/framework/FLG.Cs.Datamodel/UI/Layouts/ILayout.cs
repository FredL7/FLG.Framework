namespace FLG.Cs.Datamodel.UI.Layouts {
    public interface ILayout {
        public string Name { get; }
        public ILayoutElement Root { get; }

        void AddObserver(ILayoutObserver observer);
        public ILayoutElement GetTarget(string name);
    }
}
