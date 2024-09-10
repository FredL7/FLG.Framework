namespace FLG.Cs.Graph {
    /*
     * Graph that uses an adjencency matrix to represent paths between nodes
     * Useful when you want to find the path between two nodes that are not direct neighbours.
     */
    public class MatrixGraph<T> : Graph<T> where T : INodeItem {
        private readonly Pathfinder<T> _pathfinder;

        public MatrixGraph(T[] items, bool unidirectionality = false, int nbExpectedEdges = 0) : base(items, nbExpectedEdges, unidirectionality)
        {
            _pathfinder = new(Nodes);
        }

        public void MakeSpanningTree(SpanningTreeAlgorithm algo, Node<T> start)
        {
            List<Edge<T>> edges = algo switch
            {
                SpanningTreeAlgorithm.Kruskal => SpanningTree<T>.KruskalAlgorithm(Nodes),
                SpanningTreeAlgorithm.Prim => SpanningTree<T>.PrimAlgorithm(Nodes, start),
                _ => throw new ArgumentException($"Unknown algorithm {algo}"),
            };

            foreach(Edge<T> edge in edges)
            {
                Node<T>.SetNeighbours(edge);
            }
        }

        // This will compute all the adjencies (edges must already be defined)
        public void PopulateAdjencyList()
        {
            _pathfinder.Populate();
        }

#if DEBUG
        public void ExportTablesToCSV(string dir)
        {
            _pathfinder.ExportTablesToCSV(dir);
        }
#endif

        public List<Node<T>>? GetPath(Node<T> start, Node<T> end)
        {
            List<int>? pathID = _pathfinder.GetPath(start.ID, end.ID);
            if (pathID == null)
            {
                return null;
            }

            List<Node<T>> path = new();
            for (int i = 0; i < pathID.Count; i++)
            {
                path.Add(Nodes[pathID[i]]);
            }
            return path;
        }

        // TODO: Get neighbours(s) closer than x (might be easier with Pathfinder._distances)
    }
}
