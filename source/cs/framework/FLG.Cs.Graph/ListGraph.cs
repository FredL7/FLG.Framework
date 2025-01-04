namespace FLG.Cs.Graph {
    /*
     * Graph that uses an adjacency list to represent the neighbours of a node.
     * Treated as a sort of linked list that allows branching paths for multiple neighbours.
     */
    public class ListGraph<T>(T[] items) : Graph<T>(items, 1) where T : INodeItem {
    }
}
