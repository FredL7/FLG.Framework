using System.Numerics;

namespace FLG.Cs.Graph {
    internal interface INodeItem {
        public void SetNode<T, U>(T node) where T : Node<U> where U : INodeItem;
        public Vector3 Position { get; }
        public float DistanceFn(Vector3 other);
    }
}
