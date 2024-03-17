using System.Numerics;


namespace FLG.Cs.Math {
    /*
    public class XForm2D {
        public Vector2 Position { get; set; }
        public Vector2 Dimensions { get; set; }

        public XForm2D() { }
        public XForm2D(Vector2 position, Vector2 dimensions)
        {
            Position = position;
            Dimensions = dimensions;
        }
    }
    */

    /*
    public class XForm3D {
        public Vector3 Position { get; set; }
        public Vector3 Dimensions { get; set; }

        public XForm3D() { }
        public XForm3D(Vector3 position, Vector3 dimensions)
        {
            Position = position;
            Dimensions = dimensions;
        }
    }
    */

    public struct Size {
        public static Size Zero { get => new(Vector2.Zero); }

        private Vector2 _values;

        public float Width { get => _values.X; }
        public float Height { get => _values.Y; }

        private Size(Vector2 v) { _values = v; }

        public Size(float value)
        {
            _values = new Vector2(value);
        }

        public Size(float width, float height)
        {
            _values = new(width, height);
        }

        public override string ToString() => _values.ToString();
    }

    public struct Spacing {
        public static Spacing Zero { get => new(Vector4.Zero); }

        private Vector4 _values;

        public float Right { get => _values.X; }
        public float Top { get => _values.Y; }
        public float Left { get => _values.Z; }
        public float Bottom { get => _values.W; }

        private Spacing(Vector4 v) { _values = v; }

        public Spacing(float value)
        {
            _values = new Vector4(value);
        }

        public Spacing(float horizontal, float vertical)
        {
            _values = new Vector4(
                horizontal,
                vertical,
                horizontal,
                vertical
            );
        }

        public Spacing(float right, float left, float vertical)
        {
            _values = new Vector4(
                right,
                vertical,
                left,
                vertical
            );
        }

        public Spacing(float right, float top, float left, float bottom)
        {
            _values = new Vector4(
                right,
                top,
                left,
                bottom
            );
        }

        public override string ToString() => _values.ToString();
    }

    public class RectXform {
        private Vector2 _position;
        private Size _dimensions; // Content size
        private Size _bounds; // Includes padding

        public Spacing Margin { get; private set; }
        public Spacing Padding { get; private set; }

        public Vector2 GetWrapperPosition() => _position + new Vector2(Padding.Left, Padding.Top);
        public Size GetDimensions() => _dimensions;

        public RectXform() { }
        public RectXform(Spacing margin, Spacing padding)
        {
            _position = new();
            _dimensions = new();
            _bounds = new();
            Margin = margin;
            Padding = padding;
        }

        public void SetSizesAndPosition(Size bounds, Vector2 position)
        {
            _bounds = bounds;
            ComputeDimensions();
            _position = position;
        }

        private void ComputeDimensions()
        {
            _dimensions = new(
                _bounds.Width - (Padding.Right + Padding.Left),
                _bounds.Height - (Padding.Top + Padding.Bottom)
            );
        }
    }
}
