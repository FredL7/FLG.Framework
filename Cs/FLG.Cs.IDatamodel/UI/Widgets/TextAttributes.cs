namespace FLG.Cs.IDatamodel {
    public struct TextAttributes {
        public ETextAlignHorizontal AlignHorizontal { get; set; } = ETextAlignHorizontal.LEFT;
        public ETextAlignVertical AlignVertical { get; set; } = ETextAlignVertical.TOP;

        public TextAttributes() { }
    }
}
