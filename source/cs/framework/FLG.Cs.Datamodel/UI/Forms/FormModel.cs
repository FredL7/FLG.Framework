using System.Collections;

using FLG.Cs.Datamodel.UI.Widgets.Forms;


namespace FLG.Cs.Datamodel.UI.Forms {
    public class FormModel : IFormModel {
        private readonly Dictionary<string, IInputFieldModel> _items;
        public FormModel(List<IInputField> fields)
        {
            _items = new(fields.Count);
            foreach (var field in fields)
                _items.Add(field.Label, field.Model);
        }

        public IInputFieldModel GetItem(string label) => _items[label];

        public IEnumerator<KeyValuePair<string, IInputFieldModel>> GetEnumerator()
            => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
