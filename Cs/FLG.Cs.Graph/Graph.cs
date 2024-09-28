namespace FLG.Cs.Graph {
    public abstract class Graph<T> where T : INodeItem {
        protected T[] _items;
        protected int _expectedEdges;

        public Node<T>[] Nodes { get; private set; }

        public Graph(T[] items, int expectedNbEdges)
        {
            _items = items;
            _expectedEdges = expectedNbEdges;

            Nodes = new Node<T>[_items.Length];
            for (int i = 0; i < _items.Length; ++i)
            {
                Nodes[i] = new(i, _expectedEdges, _items[i], this);
            }
        }

        public Graph(Graph<T> copy)
        {
            _items = copy._items;
            _expectedEdges = copy._expectedEdges;

            Nodes = new Node<T>[_items.Length];
            for (int i = 0; i < _items.Length; ++i)
            {
                Nodes[i] = new(i, _expectedEdges, _items[i], this);
            }
        }

        public void SetNeighbours(List<List<int>> neighboursList)
        {
            for (int i = 0; i < neighboursList.Count; ++i)
            {
                var node = Nodes[i];
                foreach (var neighbourID in neighboursList[i])
                {
                    var neighbour = Nodes[neighbourID];
                    float weight = node.Item.WeightFn(neighbour.Item);
                    node.AddNeighbour(neighbour, weight);
                }
            }
        }
    }
}
