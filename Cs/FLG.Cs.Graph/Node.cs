namespace FLG.Cs.Graph {
    internal class Node<T> where T : INodeItem {
        private readonly Graph<T> _graph;
        private readonly List<Edge<T>> _edges;
        public int ID { get; private set; }
        public T Item { get; private set; }

        public Node(int ID, int expectedNbEdges, T item, Graph<T> graph)
        {
            _graph = graph;

            Item = item;
            item.SetNode<Node<T>, T>(this);

            this.ID = ID;
            _edges = expectedNbEdges > 0 ? new List<Edge<T>>(expectedNbEdges) : new List<Edge<T>>();
        }

        public void SetNeighbour(Node<T> destination, float weight = 1f)
        {
            // TODO: Check in Debug only
#if DEBUG
            if (this == destination)
            {
                throw new ArgumentException("Cannot create an edge between a node and itself"); // Maybe we'd want to?
            }
            // TODO: Check if nodes are already neighbours?
            // TODO: Directed
#endif

            if (!_graph.Unidirectionality)
            {
                throw new Exception("Graph cannot have unidirectional edges. Didi you meant to use SetNeighbours(node1, node2)?");
            }

            UnidirectionalEdge<T> edge = new(destination, weight);
            this._edges.Add(edge);
        }

        public static void SetNeighbours(Node<T> node1, Node<T> node2, float weight = 1f)
        {
#if DEBUG
            if (node1 == node2)
            {
                throw new ArgumentException("Cannot create an edge between a node and itself"); // Maybe we'd want to?
            }
            // TODO: Check if nodes are already neighbours?
            // TODO: Directed
#endif

            BidirectionalEdge<T> edge = new(node1, node2, weight);
            node1._edges.Add(edge);
            node2._edges.Add(edge);
        }

        public Node<T>[] GetNeighbours()
        {
            Node<T>[] neighbours = new Node<T>[_edges.Count];
            for (int i = 0; i < neighbours.Length; i++)
            {
                neighbours[i] = _edges[i].GetDestination(this);
            }
            return neighbours;
        }

        public List<Edge<T>> GetEdges() => _edges;

        public override bool Equals(object? obj)
        {
            if (obj is Node<T> other)
            {
                return ID == other.ID;
            }
            return false;
        }

        public override int GetHashCode() => ID.GetHashCode();
    }
}
