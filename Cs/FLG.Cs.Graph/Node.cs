using System.Xml.Linq;

namespace FLG.Cs.Graph {
    public class Node<T> where T : INodeItem {
        private readonly Graph<T> _graph;
        public int ID { get; private set; }
        public T Item { get; private set; }

        private HashSet<Edge<T>> _edgesHash;
        public List<Edge<T>> Edges { get; private set; }


        public Node(int ID, int expectedNbEdges, T item, Graph<T> graph)
        {
            _graph = graph;

            Item = item;
            item.SetNode<Node<T>, T>(this);

            this.ID = ID;
            _edgesHash = expectedNbEdges > 0 ? new(expectedNbEdges) : new();
            Edges = expectedNbEdges > 0 ? new(expectedNbEdges) : new();
        }

        public void AddNeighbour(Node<T> neighbour, float weight)
        {
#if DEBUG
            if (this == neighbour)
            {
                throw new ArgumentException("Cannot create an edge between a node and itself"); // Maybe we'd want to?
            }
#endif

            Edge<T> edge = new(this, neighbour, weight);
            if (!_edgesHash.Contains(edge))
            {
                Edges.Add(edge);
                _edgesHash.Add(edge);
            }
        }

        public static void SetNeighbours(Node<T> node1, Node<T> node2, float weight = 1f)
        {
#if DEBUG
            if (node1 == node2)
            {
                throw new ArgumentException("Cannot create an edge between a node and itself"); // Maybe we'd want to?
            }
            // TODO: Directed?
#endif
            Edge<T> edge = new(node1, node2, weight);
            if (!node1._edgesHash.Contains(edge))
            {
                node1.Edges.Add(edge);
                node1._edgesHash.Add(edge);
            }
            if (!node2._edgesHash.Contains(edge))
            {
                node2.Edges.Add(edge);
                node2._edgesHash.Add(edge);
            }
        }

        public static void SetNeighbours(Edge<T> edge)
        {
            // TODO: Directed?
            if (!edge.Node1._edgesHash.Contains(edge))
            {
                edge.Node1.Edges.Add(edge);
            }

            if (!edge.Node2._edgesHash.Contains(edge))
            {
                edge.Node2.Edges.Add(edge);
            }
        }

        public Node<T>[] GetNeighbours()
        {
            Node<T>[] neighbours = new Node<T>[Edges.Count];
            for (int i = 0; i < neighbours.Length; i++)
            {
                neighbours[i] = Edges[i].GetDestination(this);
            }
            return neighbours;
        }

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
