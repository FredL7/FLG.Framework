namespace FLG.Cs.Datamodel.UI.Widgets.Forms {
    public interface IFormModel : IEnumerable<KeyValuePair<string, IInputFieldModel>> {
        public IInputFieldModel GetItem(string label);
        public new IEnumerator<KeyValuePair<string, IInputFieldModel>> GetEnumerator();
    }
}
