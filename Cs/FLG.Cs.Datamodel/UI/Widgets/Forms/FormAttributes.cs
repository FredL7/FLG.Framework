namespace FLG.Cs.Datamodel {
    public struct FormAttributes {
        private const float DEFAULT_PADDINGBETWEENROWS = 10.0f;
        private const float DEFAULT_PADDINGBETWEENCOLS = 20.0f;
        private const float DEFAULT_LABELCOLWEIGHT = 1.0f;
        private const float DEFAULT_INPUTCOLWEIGHT = 1.0f;

        public float paddingBetweenRows;
        public float paddingBetweenColumns;
        public float labelColumnWeight;
        public float inputColumnWeight;

        public FormAttributes()
        {
            paddingBetweenRows = DEFAULT_PADDINGBETWEENROWS;
            paddingBetweenColumns = DEFAULT_PADDINGBETWEENCOLS;
            labelColumnWeight = DEFAULT_LABELCOLWEIGHT;
            inputColumnWeight = DEFAULT_INPUTCOLWEIGHT;
        }

        public FormAttributes(
            float paddingBetweenRows = DEFAULT_PADDINGBETWEENROWS,
            float paddingBetweenColumns = DEFAULT_PADDINGBETWEENCOLS,
            float labelColumnWeight = DEFAULT_LABELCOLWEIGHT,
            float inputColumnWeight = DEFAULT_INPUTCOLWEIGHT
        ) {
            this.paddingBetweenRows = paddingBetweenRows;
            this.paddingBetweenColumns = paddingBetweenColumns;
            this.labelColumnWeight = labelColumnWeight;
            this.inputColumnWeight = inputColumnWeight;
        }
    }
}
