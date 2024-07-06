using FLG.Cs.Math;


namespace FLG.Cs.Datamodel {
    public struct LayoutAttributes {
        private const float DEFAULT_WIDTH = 0f;
        private const float DEFAULT_HEIGHT = 0f;
        private const int DEFAULT_ORDER = 0;
        private const float DEFAULT_WEIGHT = 1f;
        private const string DEFAULT_BACKGROUNDIMAGE = "";

        public float width;
        public float height;
        public Spacing margin;
        public Spacing padding;
        public int order;
        public float weight;
        public string backgroundImage;

        public LayoutAttributes()
        {
            width = DEFAULT_WIDTH;
            height = DEFAULT_HEIGHT;
            margin = new();
            padding = new();
            order = DEFAULT_ORDER;
            weight = DEFAULT_WEIGHT;
            backgroundImage = DEFAULT_BACKGROUNDIMAGE;
        }

        public LayoutAttributes(
            float width = DEFAULT_WIDTH, float height = DEFAULT_HEIGHT,
            Spacing margin = new(), Spacing padding = new(),
            int order = DEFAULT_ORDER, float weight = DEFAULT_WEIGHT,
            string backgroundImage = DEFAULT_BACKGROUNDIMAGE
        ) {
            this.width = width;
            this.height = height;
            this.margin = margin;
            this.padding = padding;
            this.order = order;
            this.weight = weight;
            this.backgroundImage = backgroundImage;
        }
    }
}
