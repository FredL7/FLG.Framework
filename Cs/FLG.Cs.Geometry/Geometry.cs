namespace FLG.Cs.Geometry {
    public static class Geometry {
        public static int[] GetIndices(List<Triangle> triangles, bool invert)
        {
            int count = triangles.Count;
            var indices = new int[count * 3];
            for (int i = 0; i < count; ++i)
            {
                indices[i * 3 + 0] = triangles[i].v1;
                indices[i * 3 + 1] = invert ? triangles[i].v3 : triangles[i].v2;
                indices[i * 3 + 2] = invert ? triangles[i].v2 : triangles[i].v3; 
            }

            return indices;
        }

        public static Dictionary<(int, int), List<int>> BuildEdgeMap(MeshInfo mesh)
        {
            // key = (edge vA, edge vB) with v1 < v2 so (v1, v2) = (v2, v1)
            // value = list of id of triangles that are using this edge
            var edgeToTriangles = new Dictionary<(int, int), List<int>>();

            for (int i = 0; i < mesh.triangles.Length; ++i)
            {
                int v1 = mesh.triangles[i].v1;
                int v2 = mesh.triangles[i].v2;
                int v3 = mesh.triangles[i].v3;
                var edges = new[]
                {
                    // key (vA, vB) where vA < vB)
                    (Math.Min(v1, v2), Math.Max(v1, v2)),
                    (Math.Min(v2, v3), Math.Max(v2, v3)),
                    (Math.Min(v1, v3), Math.Max(v1, v3)),
                };

                foreach (var edge in edges)
                {
                    if (!edgeToTriangles.ContainsKey(edge))
                    {
                        edgeToTriangles[edge] = new List<int>();
                    }
                    edgeToTriangles[edge].Add(i);
                }
            }

            return edgeToTriangles;
        }

        public static List<List<int>> FindTriangleNeighbours(MeshInfo mesh)
        {
            // List that notes all neighbours of each triangles using their index
            var triangleNeighbours = new List<List<int>>(new List<int>[mesh.triangles.Length]);
            for (int i = 0; i < triangleNeighbours.Count; ++i)
            {
                triangleNeighbours[i] = new List<int>();
            }

            var edgeToTriangles = BuildEdgeMap(mesh);

            foreach (var pair in edgeToTriangles)
            {
                var triangleIndices = pair.Value;
                if (triangleIndices.Count > 1)
                {
                    for (int i = 0; i < triangleIndices.Count; ++i)
                    {
                        for (int j = 0; j < triangleIndices.Count; ++j)
                        {
                            if (i != j)
                            {
                                triangleNeighbours[triangleIndices[i]].Add(triangleIndices[j]);
                            }
                        }
                    }
                }
            }

            return triangleNeighbours;
        }
    }
}
