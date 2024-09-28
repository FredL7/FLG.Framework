namespace FLG.Cs.Graph {

    /*
     * Populate the whole graph at once.
     * Parse all origin-destination pairs once, which might take a long time,
     * but getting paths will then always be fast
     */
    internal class DijkstraAlgorithm<T> where T : INodeItem {
        private class Path {
            private readonly Edge<T>[] _steps;
            public Node<T> FirstStep { get; private set; }
            public Node<T> LastStep { get; private set; }

            public int Length { get => _steps.Length; }
            public float Weight { get; private set; }

            public Path(Node<T> origin, Edge<T> edge)
            {
                _steps = new Edge<T>[] { edge };

                FirstStep = edge.GetDestination(origin);
                LastStep = FirstStep;
                Weight = edge.Weight;
            }

            public Path(Path path, Edge<T> other)
            {
                _steps = new Edge<T>[path.Length + 1];
                Array.Copy(path._steps, _steps, path.Length);
                _steps[path.Length] = other;

                FirstStep = path.FirstStep;
                LastStep = other.GetDestination(path.LastStep);
                Weight = path.Weight + other.Weight;
            }
        }

        internal static void Populate(Node<T>[] nodes, int[,] adjacency, float[,] weights, int start)
        {
            PriorityQueue<Path, float> queue = new();

            foreach (var edge in nodes[start].Edges)
            {
                Path path = new(nodes[start], edge);
                queue.Enqueue(path, path.Weight);
            }

            HashSet<Node<T>> visited = new()
            {
                nodes[start]
            };

            while (queue.Count > 0)
            {
                Path currentPath = queue.Dequeue();
                Node<T> currentNode = nodes[currentPath.LastStep.ID];

                if (visited.Contains(currentNode))
                {
                    continue;
                }
                visited.Add(currentNode);

                if (currentPath.Weight < weights[start, currentNode.ID])
                {
                    weights[start, currentNode.ID] = currentPath.Weight;
                    adjacency[start, currentNode.ID] = currentPath.FirstStep.ID;
                }

                foreach (Edge<T> edge in currentNode.Edges)
                {
                    Path newPath = new(currentPath, edge);
                    queue.Enqueue(newPath, newPath.Weight);
                }

                /*if (currentPath.Length == 1)
                {
                    Node<T> destination = currentPath.LastStep;
                    weights[start, destination.ID] = currentPath.Weight;
                    adjacency[start, destination.ID] = destination.ID;
                }

                foreach (Edge<T> edge in current.Edges)
                {
                    Node<T> neighbour = edge.GetDestination(current);
                    float weight = currentPath.Weight + edge.Weight;

                    if (weight < weights[start, neighbour.ID])
                    {
                        weights[start, neighbour.ID] = weight;
                        Path newPath = new(currentPath, edge);
                        queue.Enqueue(newPath, weight);
                        adjacency[start, neighbour.ID] = newPath.FirstStep.ID;
                    }
                }*/
            }
        }
    }
}
