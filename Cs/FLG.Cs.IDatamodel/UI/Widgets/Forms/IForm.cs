namespace FLG.Cs.IDatamodel {
    public interface IForm : ILayoutElement {
        public FormModel Model { get; }
        public Action SubmitAction { get; }
        // public List<IResult> ValidateFields();
    }
}
