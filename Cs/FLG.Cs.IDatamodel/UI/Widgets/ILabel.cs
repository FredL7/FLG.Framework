namespace FLG.Cs.IDatamodel {
    public interface ILabel : ILayoutElement {
        string Text { get; }
        public ETextAlignHorizontal AlignHorizontal { get; }
        public ETextAlignVertical AlignVertical { get; }
    }
}
