namespace FLG.Cs.Graph {
    /*
     * Graph that uses an adjacency list to represent the neighbours of a node.
     * Treated as a sort of linked list that allows branching paths for multiple neighbours.
     */
    internal class ListGraph<T> : Graph<T> where T : INodeItem {
        // TODO: Get nth neighbour(s)
        public ListGraph(T[] items, bool unidirectionality) : base(items, 1, unidirectionality) { }
    }
}
