namespace FLG.Cs.Graph {
    public abstract class Graph<T> where T : INodeItem {
        public Node<T>[] Nodes { get; private set; }
        public bool Unidirectionality { get; private set; }

        public Graph(T[] items, int expectedNbEdges, bool unidirectionality)
        {
            Unidirectionality = unidirectionality;

            Nodes = new Node<T>[items.Length];
            for (int i = 0; i < items.Length; ++i)
            {
                Nodes[i] = new(i, expectedNbEdges, items[i], this);
            }
        }
    }
}
