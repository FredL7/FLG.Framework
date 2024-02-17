namespace FLG.Cs.IDatamodel {
    public interface ILayout {
        void AddObserver(ILayoutObserver observer);
        string GetName();
        public ILayoutElement GetRoot();
    }
}
