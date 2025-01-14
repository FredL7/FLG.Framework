using FLG.Cs.Datamodel.UI.Layouts;

namespace FLG.Cs.Datamodel.UI.Widgets {
    public interface IButton : ILayoutElement {
        string Text { get; }
        Action Action { get; }
    }
}
