using Godot;

using FLG.Cs.IDatamodel;


namespace FLG.Godot.UI {
    public interface IWidget<T> where T : ILayoutElement {
        public T Widget { get; }
        public Node Draw(Node parent, bool fromEditor);
    }
}
