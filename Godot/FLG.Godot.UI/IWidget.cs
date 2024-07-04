using Godot;

using FLG.Cs.Datamodel;


namespace FLG.Godot.UI {
    public interface IWidget<T> where T : ILayoutElement {
        public T Widget { get; }
        public Node Draw(Node parent, bool fromEditor);
    }
}
