using System.Collections;

namespace FLG.Cs.Datamodel {
    public class FormModel : IEnumerable<KeyValuePair<string, IInputFieldModel>> {
        private readonly Dictionary<string, IInputFieldModel> _items;
        public FormModel(List<IInputField> fields)
        {
            _items = new(fields.Count);
            foreach(var field in fields)
                _items.Add(field.Label, field.Model);
        }

        public IInputFieldModel GetItem(string label) => _items[label];

        public IEnumerator<KeyValuePair<string, IInputFieldModel>> GetEnumerator()
            => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
