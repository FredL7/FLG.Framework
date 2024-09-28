using System.Numerics;

namespace FLG.Cs.Geometry {
    public struct MeshInfo {
        public Vector3[] vertices;
        public Vector3[] normals;
        public Vector2[] uvs;
        public int[] indices;
        public Triangle[] triangles;
    }

    public struct Triangle
    {
        public int v1, v2, v3;

        public Triangle(int v1, int v2, int v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
    }
}
