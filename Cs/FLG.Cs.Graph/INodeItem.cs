using System.Numerics;

namespace FLG.Cs.Graph {
    public interface INodeItem {
        public int ID { get; }
        public void SetNode<T, U>(T node) where T : Node<U> where U : INodeItem;
        public Vector3 Position { get; }
        public float WeightFn(INodeItem other);
        public float HeuristicFn(INodeItem other);
    }
}
