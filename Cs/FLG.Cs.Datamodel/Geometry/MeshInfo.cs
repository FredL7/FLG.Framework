using System.Numerics;

namespace FLG.Cs.Datamodel {
    public struct MeshInfo {
        public Vector3[] vertices;
        public Vector3[] normals;
        public Vector2[] uvs;
        public int[] triangles;
    }
}
