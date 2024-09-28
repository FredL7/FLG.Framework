namespace FLG.Cs.Graph {
    /*
     * Graph that uses an adjencency matrix to represent paths between nodes
     * Useful when you want to find the path between two nodes that are not direct neighbours.
     */
    public class MatrixGraph<T> : Graph<T> where T : INodeItem {
        private readonly Pathfinder<T> _pathfinder;

        public MatrixGraph(T[] items, int nbExpectedEdges = 0) : base(items, nbExpectedEdges)
        {
            _pathfinder = new(Nodes);
        }

        public MatrixGraph(MatrixGraph<T> graph) : base(graph)
        {
            _pathfinder = new(Nodes);
        }

        public List<Edge<T>> ExtractSpanningTree(SpanningTreeAlgorithm algo, int startID)
        {
            return algo switch
            {
                SpanningTreeAlgorithm.Kruskal => SpanningTree<T>.KruskalAlgorithm(this),
                SpanningTreeAlgorithm.Prim => SpanningTree<T>.PrimAlgorithm(this, startID),
                _ => throw new ArgumentException($"Unknown algorithm {algo}"),
            };
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
