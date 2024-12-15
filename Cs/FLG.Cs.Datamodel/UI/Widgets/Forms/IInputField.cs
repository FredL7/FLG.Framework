using FLG.Cs.Datamodel.UI.Layouts;


namespace FLG.Cs.Datamodel.UI.Widgets.Forms {
    public interface IInputField : ILayoutElement {
        public string Label { get; }
        public string Placeholder { get; }
        public IInputFieldModel Model { get; }
    }
}
