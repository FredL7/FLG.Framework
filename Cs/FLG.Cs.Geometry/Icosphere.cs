using System.Numerics;


namespace FLG.Cs.Geometry {
    public static class Icosphere {
        /*public int GetNbRecursionFromNbFaces(int nbFaces)
        {
            int x = 20;
            int count = 0;
            while (x < nbFaces)
            {
                x *= 4;
                ++count;
            }
            return count;
        }*/

        public static int GetNbFaceFromNbRecursions(int nbRecursions) => (int)(20 * MathF.Pow(4, nbRecursions));

        // Invert is for clockwise (invert = false) and anticlockwise (invert = true) triangle definition
        public static MeshInfo GenerateMeshInfo(int recursionLevel, float radius, bool invert)
        {
            int nbFaces = GetNbFaceFromNbRecursions(recursionLevel);
            int nbPoints = nbFaces * 3;
            List<Vector3> vertices = new(nbPoints);
            List<Triangle> faces = new(nbFaces);

            CreateIcosahedron(vertices, faces, radius);
            faces = Recursion(recursionLevel, vertices, faces, radius);
            return SetMeshProperties(vertices, faces, invert);
        }

        private static void CreateIcosahedron(List<Vector3> vertices, List<Triangle> faces, float radius)
        {
            float t = (1f + MathF.Sqrt(5f)) / 2f;

            vertices.Add(new(-1f, t, 0f));
            vertices.Add(new(1f, t, 0f));
            vertices.Add(new(-1f, -t, 0f));
            vertices.Add(new(1f, -t, 0f));

            vertices.Add(new(0f, -1f, t));
            vertices.Add(new(0f, 1f, t));
            vertices.Add(new(0f, -1f, -t));
            vertices.Add(new(0f, 1f, -t));

            vertices.Add(new(t, 0f, -1f));
            vertices.Add(new(t, 0f, 1f));
            vertices.Add(new(-t, 0f, -1f));
            vertices.Add(new(-t, 0f, 1f));

            for (int i = 0; i < vertices.Count; ++i)
            {
                vertices[i] = Vector3.Normalize(vertices[i]) * radius;
            }

            // 5 Faces around point 0
            faces.Add(new(0, 11, 5));
            faces.Add(new(0, 5, 1));
            faces.Add(new(0, 1, 7));
            faces.Add(new(0, 7, 10));
            faces.Add(new(0, 10, 11));

            // 5 adjacent faces
            faces.Add(new(1, 5, 9));
            faces.Add(new(5, 11, 4));
            faces.Add(new(11, 10, 2));
            faces.Add(new(10, 7, 6));
            faces.Add(new(7, 1, 8));

            // 5 faces around point 3
            faces.Add(new(3, 9, 4));
            faces.Add(new(3, 4, 2));
            faces.Add(new(3, 2, 6));
            faces.Add(new(3, 6, 8));
            faces.Add(new(3, 8, 9));

            // 5 adjacent faces
            faces.Add(new(4, 9, 5));
            faces.Add(new(2, 4, 11));
            faces.Add(new(6, 2, 10));
            faces.Add(new(8, 6, 7));
            faces.Add(new(9, 8, 1));
        }

        private static List<Triangle> Recursion(int recursionLevel, List<Vector3> vertices, List<Triangle> faces, float radius)
        {
            Dictionary<long, int> middlePointIndexCache = new();
            List<Triangle> newFaces = faces;

            for (int i = 0; i < recursionLevel; ++i)
            {
                List<Triangle> faces2 = new();
                foreach(var triangle in newFaces)
                {
                    // replace the triangle by 4 triangles
                    int a = GetMiddlePoint(triangle.v1, triangle.v2, ref vertices, ref middlePointIndexCache, radius);
                    int b = GetMiddlePoint(triangle.v2, triangle.v3, ref vertices, ref middlePointIndexCache, radius);
                    int c = GetMiddlePoint(triangle.v3, triangle.v1, ref vertices, ref middlePointIndexCache, radius);

                    faces2.Add(new(triangle.v1, a, c));
                    faces2.Add(new(triangle.v2, b, a));
                    faces2.Add(new(triangle.v3, c, b));
                    faces2.Add(new(a, b, c));
                }

                newFaces = faces2;
            }

            return newFaces;
        }

        private static int GetMiddlePoint(int p1, int p2, ref List<Vector3> vertices, ref Dictionary<long, int> cache, float radius)
        {
            // first check if we have it already in the cache
            bool firstIsSmaller = p1 < p2;
            long smallerIndex = firstIsSmaller ? p1 : p2;
            long greaterIndex = firstIsSmaller ? p2 : p1;
            long key = (smallerIndex << 32) + greaterIndex;


            if (cache.TryGetValue(key, out int value))
            {
                return value;
            }

            // Not in cache, compute it
            Vector3 point1 = vertices[p1];
            Vector3 point2 = vertices[p2];
            Vector3 middle = (point1 + point2) / 2f;

            int i = vertices.Count;
            vertices.Add(Vector3.Normalize(middle) * radius);
            cache.Add(key, i);

            return i;
        }

        private static MeshInfo SetMeshProperties(List<Vector3> vertices, List<Triangle> faces, bool invert)
        {
            Vector3[] normalizedVertices = new Vector3[vertices.Count];
            for (int i = 0; i < vertices.Count; ++i)
            {
                normalizedVertices[i] = Vector3.Normalize(vertices[i]);
            }

            int[] indices = Geometry.GetIndices(faces, invert);

            Vector2[] uvs = new Vector2[vertices.Count];
            for (int i = 0; i < uvs.Length; ++i)
            {
                var unitVector = normalizedVertices[i];
                Vector2 icouv = new(
                    (MathF.Atan2(unitVector.X, unitVector.Z) + MathF.PI) / MathF.PI / 2,
                    (MathF.Acos(unitVector.Y) + MathF.PI) / MathF.PI - 1.0f
                );
                uvs[i] = new(icouv.X, icouv.Y);
            }

            return new()
            {
                vertices = vertices.ToArray(),
                normals = normalizedVertices,
                indices = indices,
                uvs = uvs,
                triangles = faces.ToArray()
            };
        }
    }
}
