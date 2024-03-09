namespace FLG.Cs.IDatamodel {
    public interface IButton : ILayoutElement {
        string Text { get; }
        Action Action { get; }
    }
}
