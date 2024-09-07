namespace FLG.Cs.Graph {
    internal abstract class Graph<T> where T : INodeItem {
        protected readonly Node<T>[] _nodes;
        public bool Unidirectionality { get; private set; }

        public Graph(T[] items, int expectedNbEdges, bool unidirectionality)
        {
            Unidirectionality = unidirectionality;

            _nodes = new Node<T>[items.Length];
            for (int i = 0; i < items.Length; ++i)
            {
                _nodes[i] = new(i, expectedNbEdges, items[i], this);
            }
        }
    }
}
