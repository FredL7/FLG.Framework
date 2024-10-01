using FLG.Cs.FLGMath;


namespace FLG.Cs.Geometry {
    public struct MeshInfo {
        public FLGVector3[] vertices;
        public FLGVector3[] normals;
        public FLGVector2[] uvs;
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
