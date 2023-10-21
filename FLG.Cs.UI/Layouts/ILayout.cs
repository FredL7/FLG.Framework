namespace FLG.Cs.UI.Layouts {
    public interface ILayout {
        void AddObserver(ILayoutObserver observer);
        public ILayoutElement GetRoot();
    }
}
