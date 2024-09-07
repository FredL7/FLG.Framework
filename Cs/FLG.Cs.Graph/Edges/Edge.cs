namespace FLG.Cs.Graph {
    internal abstract class Edge<T> where T : INodeItem {
        public float Weight { get; private set; }
        public abstract bool Bidirectional { get; }

        public Edge(float weight)
        {
            Weight = weight;
        }

        public abstract Node<T> GetDestination(Node<T> origin);
    }
}
