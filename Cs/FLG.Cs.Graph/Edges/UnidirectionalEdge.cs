namespace FLG.Cs.Graph {
    internal class UnidirectionalEdge<T> : Edge<T> where T : INodeItem {
        public Node<T> Destination { get; private set; }
        public override bool Bidirectional { get => false; }

        public UnidirectionalEdge(Node<T> destination, float weight = 1f)
            : base(weight)
        {
            Destination = destination;
        }

        public override Node<T> GetDestination(Node<T> _) => Destination;
    }
}
