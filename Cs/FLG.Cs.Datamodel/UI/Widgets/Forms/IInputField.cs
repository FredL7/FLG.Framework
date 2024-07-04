namespace FLG.Cs.Datamodel {
    public interface IInputField : ILayoutElement {
        public string Label { get; }
        public string Placeholder { get; }
        public IInputFieldModel Model { get; }
    }
}
