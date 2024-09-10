using System.Text;

namespace FLG.Cs.Graph {
    internal class Pathfinder<T> where T : INodeItem {
        protected const int PATH_UNDEFINED = -2;
        protected const int PATH_UNAVAILABLE = -1;

        protected Node<T>[] _nodes;
        protected int[,] _adjacency;
        protected float[,] _weights;
        // TODO: (Debugging only?) Helper to dump _adjacency to csv file

        public Pathfinder(Node<T>[] nodes)
        {
            _nodes = nodes;
            _adjacency = new int[nodes.Length, nodes.Length];
            _weights = new float[nodes.Length, nodes.Length];
            for (int i = 0; i < nodes.Length; ++i)
            {
                for (int j = 0; j < nodes.Length; ++j)
                {
                    _adjacency[i, j] = PATH_UNDEFINED;
                    _weights[i, j] = int.MaxValue;
                }
            }

            for (int i = 0; i < _nodes.Length; ++i)
            {
                _adjacency[i, i] = i;
                _weights[i, i] = 0f;
            }
        }

        internal void Populate()
        {
            for (int start = 0; start < _adjacency.GetLength(0); ++start)
            {
                DijkstraAlgorithm<T>.Populate(_nodes, _adjacency, _weights, start);
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

#if DEBUG
        internal void ExportTablesToCSV(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            StringBuilder positions = new();
            foreach(var _node in _nodes)
            {
                positions.Append(_node.Item.Position);
                positions.Append('\n');
            }
            File.WriteAllText(dir + "positions.txt", positions.ToString());

            string adjacencyCSV = ConvertToCSV(_adjacency);
            string distancesCSV = ConvertToCSV(_weights);
            File.WriteAllText(dir + "adjacency.csv", adjacencyCSV);
            File.WriteAllText(dir + "distances.csv", distancesCSV);
        }

        private static string ConvertToCSV<U>(U[,] data)
        {
            StringBuilder sb = new();
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);

            sb.Append("A,");
            for (int i = 0; i < rows; ++i)
            {
                if (i > 0)
                {
                    sb.Append(',');
                }
                sb.Append(i);
            }
            sb.AppendLine();

            for (int i = 0; i < rows; ++i)
            {
                sb.Append(i);
                sb.Append(',');
                for (int j = 0; j < cols; ++j)
                {
                    if (j > 0)
                    {
                        sb.Append(',');
                    }
                    sb.Append(data[i, j]);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
#endif
    }
}
