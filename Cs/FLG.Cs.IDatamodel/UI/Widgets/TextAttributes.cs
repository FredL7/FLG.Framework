namespace FLG.Cs.IDatamodel {
    public struct TextAttributes {
        private const ETextAlignHorizontal DEFAULT_ALIGNHORIZONTAL = ETextAlignHorizontal.LEFT;
        private const ETextAlignVertical DEFAULT_ALIGNVERTICAL = ETextAlignVertical.TOP;

        public ETextAlignHorizontal alignHorizontal;
        public ETextAlignVertical alignVertical;

        public TextAttributes()
        {
            alignHorizontal = DEFAULT_ALIGNHORIZONTAL;
            alignVertical = DEFAULT_ALIGNVERTICAL;
        }

        public TextAttributes(
            ETextAlignHorizontal alignHorizontal = DEFAULT_ALIGNHORIZONTAL,
            ETextAlignVertical alignVertical = DEFAULT_ALIGNVERTICAL)
        {
            this.alignHorizontal = alignHorizontal;
            this.alignVertical = alignVertical;
        }
    }
}
