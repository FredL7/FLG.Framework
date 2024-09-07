namespace FLG.Cs.Graph {
    internal class DijkstraAlgorithm<T> where T : INodeItem {
        class Path
        {
            public int[] _steps;
            public int FirstStep { get => _steps[1]; }
            public int Length { get => _steps.Length; }
            public float Distance { get; private set; }

            public Path(Node<T> origin)
            {
                _steps = new int[] { origin.ID };
                Distance = 0f;
            }

            public Path(Path path, Node<T> newDestination)
            {
                _steps = new int[path.Length + 1];
                Array.Copy(path._steps, _steps, path.Length);
                _steps[path.Length] = newDestination.ID;
            }

            public int Last() => _steps.Last();
        }

        internal void Populate(Node<T>[] nodes, int[,] adjacency, float[,] distances, int start)
        {
            PriorityQueue<Path, float> queue = new();
            Path path = new(nodes[start]);
            queue.Enqueue(path, path.Length);

            HashSet<Node<T>> visited = new();

            while (queue.Count > 0)
            {
                Path currentPath = queue.Dequeue();
                Node<T> current = nodes[currentPath.Last()];

                if (visited.Contains(current))
                {
                    continue;
                }
                visited.Add(current);

                foreach(Edge<T> edge in current.GetEdges())
                {
                    Node<T> neighbour = edge.GetDestination(current);
                    float distance = distances[start, current.ID] + edge.Weight;

                    if (distance < distances[start, neighbour.ID])
                    {
                        distances[start, neighbour.ID] = distance;
                        Path newPath = new(path, neighbour);
                        queue.Enqueue(newPath, distance);
                        adjacency[start, neighbour.ID] = newPath.FirstStep;
                    }
                }
            }
        }
    }
}
