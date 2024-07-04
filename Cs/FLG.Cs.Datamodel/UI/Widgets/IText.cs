namespace FLG.Cs.Datamodel {
    public interface IText : ILayoutElement {
        string Content { get; set; }
        public ETextAlignHorizontal AlignHorizontal { get; }
        // public ETextAlignVertical AlignVertical { get; } => No BBCode for vertical alignment

        public event EventHandler TextChanged;
    }
}
