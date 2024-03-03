namespace FLG.Cs.IDatamodel {
    public interface IText : ILayoutElement {
        string Content { get; set; }
        public event EventHandler TextChanged;
    }
}
