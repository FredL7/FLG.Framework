using FLG.Cs.Math;


namespace FLG.Cs.Geometry {
    public struct Triangle(int v1, int v2, int v3) {
        public int v1 = v1, v2 = v2, v3 = v3;
    }

    public struct MeshInfo {
        public FLGVector3[] vertices;
        public FLGVector3[] normals;
        public FLGVector2[] uvs;
        public int[] indices;
        public Triangle[] triangles;
    }
}
