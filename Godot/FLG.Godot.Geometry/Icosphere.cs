using Godot;

using FLG.Cs.Datamodel;

using gd = Godot;


namespace FLG.Godot.Geometry {
    public partial class Icosphere : Node {
        public void Draw(MeshInfo meshInfo)
        {
            var arrayMesh = new ArrayMesh();
            var arrays = new gd.Collections.Array();
            arrays.Resize((int)Mesh.ArrayType.Max);
            arrays[(int)Mesh.ArrayType.Vertex] = Array.ConvertAll(meshInfo.vertices, v => new gd.Vector3(v.X, v.Y, v.Z));
            arrays[(int)Mesh.ArrayType.Normal] = Array.ConvertAll(meshInfo.normals, n => new gd.Vector3(n.X, n.Y, n.Z));
            arrays[(int)Mesh.ArrayType.TexUV] = Array.ConvertAll(meshInfo.uvs, uv => new gd.Vector2(uv.X, uv.Y));
            arrays[(int)Mesh.ArrayType.Index] = meshInfo.triangles;

            arrayMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arrays);
            var mesh = new MeshInstance3D();
            mesh.Mesh = arrayMesh;

            AddChild(mesh);
            mesh.Owner = this;
        }
    }
}
