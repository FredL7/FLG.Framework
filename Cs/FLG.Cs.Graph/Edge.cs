namespace FLG.Cs.Graph {
    internal class Edge {
        public Node Node1 { private get; set; }
        public Node Node2 { private get; set; }
        public float Weight { get; set; }

        // TODO: Directed?

        public Edge(Node node1, Node node2, float weight = 1f)
        {
            Node1 = node1;
            Node2 = node2;
            Weight = weight;
        }

        public Node GetOther(Node node)
        {
            if (Node1 == node)
            {
                return Node2;
            }
            else if (Node2 == node)
            {
                return Node1;
            }
            else
            {
                throw new ArgumentException($"Node {node} is not part of this edge");
            }
        }
    }
}
