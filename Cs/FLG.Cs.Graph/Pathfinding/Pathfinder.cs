namespace FLG.Cs.Graph {
    internal class Pathfinder<T> where T : INodeItem {
        protected const int PATH_UNDEFINED = -2;
        protected const int PATH_UNAVAILABLE = -1;

        protected Node<T>[] _nodes;
        protected int[,] _adjacency;
        protected float[,] _distances;
        // TODO: (Debugging only?) Helper to dump _adjacency to csv file

        public Pathfinder(Node<T>[] nodes)
        {
            _nodes = nodes;
            _adjacency = new int[nodes.Length, nodes.Length];
            _distances = new float[nodes.Length, nodes.Length];
            for (int i = 0; i < nodes.Length; ++i)
            {
                for (int j = 0; j < nodes.Length; ++j)
                {
                    _adjacency[i, j] = PATH_UNDEFINED;
                    _distances[i, j] = int.MaxValue;
                }
            }

            for (int i = 0; i < _nodes.Length; ++i)
            {
                _adjacency[i, i] = i;
                _distances[i, i] = 0f;
            }
        }

        internal void Populate()
        {
            DijkstraAlgorithm<T> algo = new();
            for (int start = 0; start < _adjacency.Length; ++start)
            {
                algo.Populate(_nodes, _adjacency, _distances, start);
            }
        }

        // Returns null if there is no path between start and end
        // Returns a list of length 1 if start and end are the same
        internal List<int>? GetPath(int start, int end)
        {
            if (start == end)
            {
                return new List<int>() { start };
            }

            if (_adjacency[start, end] == PATH_UNDEFINED)
            {
                // TODO: AStar(start, end);
            }

            if (_adjacency[start, end] == PATH_UNAVAILABLE)
            {
                return null;
            }

            List<int> path = new()
            {
                start
            };

            int currentStep = start;
            int nextStep;
            do
            {
                nextStep = _adjacency[currentStep, end];
                path.Add(nextStep);
                currentStep = nextStep;
            }
            while (path.Last() != end);

            return path;
        }
    }
}

/*
 * The pathfinder class isn't bound to a spacific algorithm
 * Instead, it will use the most appropriate according to the user's need
 * 
 * When Populating a whole graph is requested, use Dijkstra
 * When finding the path between 2 nodes, use A* with bidirectional search
 * 
 * Always check first if the path already exists before using the algorithm
 */
