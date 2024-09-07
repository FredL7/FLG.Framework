namespace FLG.Cs.Graph {
    internal class BidirectionalEdge<T> : Edge<T> where T : INodeItem {
        public Node<T> Node1 { get; private set; }
        public Node<T> Node2 { get; private set; }
        public override bool Bidirectional { get => true; }

        public BidirectionalEdge(Node<T> node1, Node<T> node2, float weight = 1f)
            : base(weight)
        {
            Node1 = node1;
            Node2 = node2;
        }

        public override Node<T> GetDestination(Node<T> origin)
        {
            if (Node1 == origin)
            {
                return Node2;
            }
            else if (Node2 == origin)
            {
                return Node1;
            }
            else
            {
                throw new ArgumentException($"Node {origin} is not part of this edge");
            }
        }
    }
}
