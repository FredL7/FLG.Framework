namespace FLG.Cs.Graph {
    public class Edge<T>(Node<T> node1, Node<T> node2, float weight = 1f) where T : INodeItem {
        public float Weight { get; private set; } = weight;
        public Node<T> Node1 { get; private set; } = node1;
        public Node<T> Node2 { get; private set; } = node2;

        public Node<T> GetDestination(Node<T> origin)
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

        public override bool Equals(object? obj)
        {
            if (obj is Edge<T> other)
            {
                return (Node1.ID == other.Node1.ID && Node2.ID == other.Node2.ID) ||
                       (Node1.ID == other.Node2.ID && Node2.ID == other.Node1.ID);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hashNode1 = Node1.ID;
            int hashNode2 = Node2.ID;

            return hashNode1 < hashNode2 ? (hashNode1 * 397) ^ hashNode2 : (hashNode2 * 397) ^ hashNode2;
            // Could use larger prime like 1009, 2011, or 3011 but should test hash distribution before optimization
        }
    }
}
