namespace FLG.Cs.IDatamodel {
    public struct GridAttributes {
        public EGridDirection Direction { get; set; } = EGridDirection.NORMAL;
        public EGridJustify Justify { get; set; } = EGridJustify.START;
        public EGridAlignment Alignment { get; set; } = EGridAlignment.START;

        public GridAttributes() { }
    }
}
