namespace FLG.Cs.Math {
    public struct Size {
        public static Size Zero { get => new(FLGVector2.Zero); }

        private FLGVector2 _value;

        public readonly float Width => _value.X;
        public readonly float Height => _value.Y;

        private Size(FLGVector2 v) { _value = v; }

        public Size(float value)
        {
            _value = new FLGVector2(value, value);
        }

        public Size(float width, float height)
        {
            _value = new(width, height);
        }

        public override readonly string ToString() => _value.ToString();
    }

    public struct Spacing {
        public static Spacing Zero { get => new(FLGVector4.Zero); }

        private FLGVector4 _values;

        public readonly float Right => _values.X;
        public readonly float Top => _values.Y;
        public readonly float Left => _values.Z;
        public readonly float Bottom => _values.W;

        private Spacing(FLGVector4 v) { _values = v; }

        public Spacing(float value)
        {
            _values = new FLGVector4(value, value, value, value);
        }

        public Spacing(float horizontal, float vertical)
        {
            _values = new FLGVector4(
                horizontal,
                vertical,
                horizontal,
                vertical
            );
        }

        public Spacing(float right, float left, float vertical)
        {
            _values = new FLGVector4(
                right,
                vertical,
                left,
                vertical
            );
        }

        public Spacing(float right, float top, float left, float bottom)
        {
            _values = new FLGVector4(
                right,
                top,
                left,
                bottom
            );
        }

        public override readonly string ToString() => _values.ToString();
    }

    public class RectXform {
        private FLGVector2 _position;
        private Size _dimensions; // Content size
        private Size _bounds; // Includes padding

        public Spacing Margin { get; private set; }
        public Spacing Padding { get; private set; }

        public FLGVector2 GetWrapperPosition() => _position + new FLGVector2(Padding.Left, Padding.Top);
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

        public void SetSizesAndPosition(Size bounds, FLGVector2 position)
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
