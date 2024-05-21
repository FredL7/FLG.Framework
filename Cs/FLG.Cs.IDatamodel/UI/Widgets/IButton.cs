namespace FLG.Cs.Datamodel {
    public interface IButton : ILayoutElement {
        string Text { get; }
        Action Action { get; }
    }
}
