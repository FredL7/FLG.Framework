using FLG.Cs.Math;


namespace FLG.Cs.IDatamodel {
    public struct LayoutAttributes {
        public float Width { get; set; } = 0f;
        public float Height { get; set; } = 0f;
        public Spacing Margin { get; set; } = new();
        public Spacing Padding { get; set; } = new();
        public int Order { get; set; } = 0;
        public float Weight { get; set; } = 1f;

        public LayoutAttributes() { }
    }
}
