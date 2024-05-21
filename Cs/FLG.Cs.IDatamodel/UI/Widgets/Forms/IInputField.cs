namespace FLG.Cs.IDatamodel {
    public interface IInputField : ILayoutElement {
        public string Label { get; }
        public string Placeholder { get; }
        public IInputFieldModel Model { get; }
    }
}
