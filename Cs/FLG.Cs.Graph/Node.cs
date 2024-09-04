namespace FLG.Cs.Graph {
    internal class Node {
        private List<Edge> _edges;

        public uint ID {  private set; get; }

        public static void SetNeighbours(Node node1, Node node2, float weight = 1f)
        {
#if DEBUG
            if (node1 == node2)
            {
                throw new ArgumentException("Cannot create an edge between a node and itself"); // Maybe we'd want to?
            }
            // TODO: Check if nodes are already neighbours?
            // TODO: Directed
#endif

            Edge edge = new(node1, node2, weight);
            node1._edges.Add(edge);
            node2._edges.Add(edge);
        }

        public Node(uint id, int nbNeighbours = 0)
        {
            ID = id;
            _edges = nbNeighbours > 0 ? new List<Edge>(nbNeighbours) : new List<Edge>();
        }

        public Node[] GetNeighbours()
        {
            Node[] neighbours = new Node[_edges.Count];
            for (int i = 0; i < neighbours.Length; i++)
            {
                neighbours[i] = _edges[i].GetOther(this);
            }
            return neighbours;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Node other)
            {
                return ID == other.ID;
            }
            return false;
        }

        public override int GetHashCode() => ID.GetHashCode();
    }
}
