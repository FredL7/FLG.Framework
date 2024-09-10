using System.Numerics;

namespace FLG.Cs.Graph {
    public enum SpanningTreeAlgorithm
    {
        Kruskal, Prim
    }

    internal static class SpanningTree<T> where T : INodeItem {
        /*
        Algorithm Kruskal(Graph G):
            // G is the input graph with vertices and edges
            Initialize an empty list T to hold the edges of the spanning tree
            Sort all edges in G by weight in ascending order
            Initialize a disjoint-set (or union-find) data structure

            For each edge (u, v) in the sorted edge list:
                If u and v are not in the same subset in the disjoint-set:
                    Add the edge (u, v) to T
                    Union the subsets containing u and v

            Return T // T is the spanning tree
        */
        internal static List<Edge<T>> KruskalAlgorithm(Node<T>[] nodes)
        {
            throw new NotImplementedException();

            /*
            PriorityQueue<Edge<T>, float> queue = new();
            List<Edge<T>> edges = new();
            for (int i = 0; i < nodes.Length; ++i)
            {
                for (int j = i + 1; j < nodes.Length; ++j)
                {
                    float distance = Vector3.Distance(nodes[i].Item.Position, nodes[j].Item.Position);
                    Edge<T> edge = new(nodes[i], nodes[j], distance);
                    queue.Enqueue(edge, distance);
                }
            }

            return edges;
            */
        }

        internal static List<Edge<T>> PrimAlgorithm(Node<T>[] nodes, Node<T> start)
        {
            PriorityQueue<Edge<T>, float> queue = new();
            HashSet<Node<T>> visited = new(); // TODO: Could be replaced by bool[] since I have an id
            List<Edge<T>> edges = new();

            visited.Add(start);
            for (int j = 1; j < nodes.Length; ++j)
            {
                float weight = nodes[0].Item.WeightFunction(nodes[j].Item);
                Edge<T> edge = new(nodes[0], nodes[j], weight);
                queue.Enqueue(edge, edge.Weight);
            }

            while (queue.Count > 0)
            {
                Edge<T> current = queue.Dequeue();
                Node<T> destination = current.Node2; // using Node2 because the edges are unidirectional in this case
                if (!visited.Contains(destination))
                {
                    edges.Add(current);
                    visited.Add(destination);

                    foreach(Node<T> node in nodes)
                    {
                        if (!visited.Contains(node))
                        {
                            float distance = destination.Item.WeightFunction(node.Item);
                            Edge<T> edge = new(destination, node, distance);
                            queue.Enqueue(edge, edge.Weight);
                        }
                    }
                }
            }

            return edges;
        }
    }
}
