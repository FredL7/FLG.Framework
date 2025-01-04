using System.Numerics;


namespace FLG.Cs.Math {
    public struct FLGVector2 {
        private Vector2 _v;

        public static FLGVector2 Zero => new(0, 0);

        public readonly float X => _v.X;
        public readonly float Y => _v.Y;

        public FLGVector2(float x, float y)
        {
            _v = new(x, y);
        }

        private FLGVector2(Vector2 v)
        {
            _v = v;
        }

        public static FLGVector2 Normalize(FLGVector2 v) => new(Vector2.Normalize(v._v));

        public static FLGVector2 operator +(FLGVector2 a, FLGVector2 b) => new(a._v + b._v);
        public static FLGVector2 operator *(FLGVector2 a, float b) => new(a._v * b);
        public static FLGVector2 operator *(float b, FLGVector2 a) => new(a._v * b);
        public static FLGVector2 operator /(FLGVector2 a, float b) => new(a._v / b);

        public override readonly string ToString() => $"({X}, {Y})";
    }

    public struct FLGVector3 {
        private Vector3 _v;

        public static FLGVector3 Zero => new(0, 0, 0);

        public readonly float X => _v.X;
        public readonly float Y => _v.Y;
        public readonly float Z => _v.Z;

        public FLGVector3(float x, float y, float z)
        {
            _v = new(x, y, z);
        }

        private FLGVector3(Vector3 v)
        {
            _v = v;
        }

        public static FLGVector3 Normalize(FLGVector3 v) => new(Vector3.Normalize(v._v));

        public static FLGVector3 operator +(FLGVector3 a, FLGVector3 b) => new(a._v + b._v);
        public static FLGVector3 operator *(FLGVector3 a, float b) => new(a._v * b);
        public static FLGVector3 operator *(float b, FLGVector3 a) => new(a._v * b);
        public static FLGVector3 operator /(FLGVector3 a, float b) => new(a._v / b);

        public override readonly string ToString() => $"({X}, {Y}, {Z})";
    }

    public struct FLGVector4 {
        private Vector4 _v;

        public static FLGVector4 Zero => new(0, 0, 0, 0);

        public readonly float X => _v.X;
        public readonly float Y => _v.Y;
        public readonly float Z => _v.Z;
        public readonly float W => _v.W;

        public FLGVector4(float x, float y, float z, float w)
        {
            _v = new(x, y, z, w);
        }

        private FLGVector4(Vector4 v)
        {
            _v = v;
        }

        public static FLGVector4 Normalize(FLGVector4 v) => new(Vector4.Normalize(v._v));

        public static FLGVector4 operator +(FLGVector4 a, FLGVector4 b) => new(a._v + b._v);
        public static FLGVector4 operator *(FLGVector4 a, float b) => new(a._v * b);
        public static FLGVector4 operator *(float b, FLGVector4 a) => new(a._v * b);
        public static FLGVector4 operator /(FLGVector4 a, float b) => new(a._v / b);

        public override readonly string ToString() => $"({X}, {Y}, {Z})";
    }
}
