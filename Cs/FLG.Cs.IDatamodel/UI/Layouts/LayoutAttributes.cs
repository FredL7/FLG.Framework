using FLG.Cs.Math;


namespace FLG.Cs.IDatamodel {
    public struct LayoutAttributes {
        private const float DEFAULT_WIDTH = 0f;
        private const float DEFAULT_HEIGHT = 0f;
        private const int DEFAULT_ORDER = 0;
        private const float DEFAULT_WEIGHT = 1f;

        public float width;
        public float height;
        public Spacing margin;
        public Spacing padding;
        public int order;
        public float weight;

        public LayoutAttributes()
        {
            width = DEFAULT_WIDTH;
            height = DEFAULT_HEIGHT;
            margin = new();
            padding = new();
            order = DEFAULT_ORDER;
            weight = DEFAULT_WEIGHT;
        }

        public LayoutAttributes(
            float width = DEFAULT_WIDTH, float height = DEFAULT_HEIGHT,
            Spacing margin = new(), Spacing padding = new(),
            int order = DEFAULT_ORDER, float weight = DEFAULT_WEIGHT
        ) {
            this.width = width;
            this.height = height;
            this.margin = margin;
            this.padding = padding;
            this.order = order;
            this.weight = weight;
        }
    }
}
