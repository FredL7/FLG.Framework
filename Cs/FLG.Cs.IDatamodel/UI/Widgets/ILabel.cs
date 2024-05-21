namespace FLG.Cs.Datamodel {
    public interface ILabel : ILayoutElement {
        string Text { get; }
        public ETextAlignHorizontal AlignHorizontal { get; }
        public ETextAlignVertical AlignVertical { get; }
    }
}
