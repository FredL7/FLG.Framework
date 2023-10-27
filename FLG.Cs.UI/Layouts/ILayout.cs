namespace FLG.Cs.UI.Layouts {
    public interface ILayout {
        void AddObserver(ILayoutObserver observer);
        string GetName();
        public ILayoutElement GetRoot();
    }
}
