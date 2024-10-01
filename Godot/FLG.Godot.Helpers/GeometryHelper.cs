using Godot;

using FLG.Cs.Geometry;
using FLG.Cs.FLGMath;

using gd = Godot;


namespace FLG.Godot.Helpers {
    public static class GeometryHelper {
        public static MeshInstance3D GetMeshInstance3D(MeshInfo meshInfo)
        {
            var arrayMesh = new ArrayMesh();
            var arrays = new gd.Collections.Array();
            arrays.Resize((int)Mesh.ArrayType.Max);
            arrays[(int)Mesh.ArrayType.Vertex] = Array.ConvertAll(meshInfo.vertices, v => new Vector3(v.X, v.Y, v.Z));
            arrays[(int)Mesh.ArrayType.Normal] = Array.ConvertAll(meshInfo.normals, n => new Vector3(n.X, n.Y, n.Z));
            arrays[(int)Mesh.ArrayType.TexUV] = Array.ConvertAll(meshInfo.uvs, uv => new Vector2(uv.X, uv.Y));
            arrays[(int)Mesh.ArrayType.Index] = meshInfo.indices;

            arrayMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arrays);
            return new MeshInstance3D
            {
                Mesh = arrayMesh
            };
        }
    }
}
