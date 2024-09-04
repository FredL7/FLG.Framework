namespace FLG.Cs.Graph {
    /*
     * Graph that uses an adjencency matrix to represent paths between nodes
     * Useful when you want to find the path between two nodes that are not direct neighbours.
     */
    internal class MatrixGraph : Graph {
        private readonly Node[] _nodes;
        private readonly float[,] _adjacency;

        public MatrixGraph(uint nbNodes)
        {
            _nodes = new Node[nbNodes];
            _adjacency = new float[nbNodes, nbNodes];
        }

        // TODO: Get neighbours(s) closer than x
    }
}
