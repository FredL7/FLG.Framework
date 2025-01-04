using FLG.Cs.Datamodel.UI.Layouts;


namespace FLG.Cs.Datamodel.UI.Widgets.Forms {
    public interface IForm : ILayoutElement {
        public IFormModel Model { get; }
    }
}
