using Godot;

using FLG.Cs.IDatamodel;


namespace FLG.Godot.UI {
    internal interface IWidget<T> where T : ILayoutElement {
        public T Widget { get; }
        public void Draw(Node node, Node root);
    }
}
