namespace FLG.Cs.IDatamodel {
    public struct GridAttributes {
        private const EGridDirection DEFAULT_DIRECTION = EGridDirection.NORMAL;
        private const EGridJustify DEFAULT_JUSTIFY = EGridJustify.START;
        private const EGridAlignment DEFAULT_ALIGNMENT = EGridAlignment.START;

        public EGridDirection direction;
        public EGridJustify justify;
        public EGridAlignment alignment;

        public GridAttributes()
        {
            direction = DEFAULT_DIRECTION;
            justify = DEFAULT_JUSTIFY;
            alignment = DEFAULT_ALIGNMENT;
        }

        public GridAttributes(
            EGridDirection direction = DEFAULT_DIRECTION,
            EGridJustify justify = DEFAULT_JUSTIFY,
            EGridAlignment alignment = DEFAULT_ALIGNMENT
        ) {
            this.direction = direction;
            this.justify = justify;
            this.alignment = alignment;
        }
    }
}
