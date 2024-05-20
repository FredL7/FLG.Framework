using FLG.Cs.IDatamodel;
using FLG.Cs.UI.Grids;
using System.Xml;

namespace FLG.Cs.UI.Widgets {
    internal class Form : Container, IForm {
        public override ELayoutElement Type { get => ELayoutElement.FORM; }

        public string Title { get; private set; }
        public FormModel Model { get; private set; }
        public Action SubmitAction { get; }

        private List<IInputField> _fields;
        private readonly FormAttributes _formAttributes;

        public Form(string name, XmlNode node) : base(name, node) {
            // Should not create Form from xml (unless we add a custom way to add childs so we can dynamically create the FormModel by using the default IInputFieldModel)
            throw new NotImplementedException();
        }

        public Form(string name, string title, List<IInputField> fields, Action submitAction, LayoutAttributes layoutAttr, FormAttributes formAttr)
            : base(name, layoutAttr)
        {
            Title = title;
            Model = new FormModel(fields);
            SubmitAction = submitAction;
            _fields = fields;
            _formAttributes = formAttr;
        }

        public sealed override void OnAddedToPage(string pageID)
        {
            BuildUI(pageID);
        }

        private void BuildUI(string pageID)
        {
            var layout = GetChildrensInternal(pageID);

            var container = new VStack(Name + "-container", new(), new());
            layout.Add(container);

            List<IInputField> newFields = new(_fields.Count);
            foreach (var field in _fields)
            {
                HStack inputLine = new(field.Name + "-inputline", new(margin: new(0, 0, 0, _formAttributes.paddingBetweenRows)), new());
                container.AddChild(inputLine, pageID);

                Label inputLabel = new(field.Name + "-label", field.Label,
                    new(margin: new(_formAttributes.paddingBetweenColumns / 2.0f, 0, 0, 0), weight: _formAttributes.labelColumnWeight),
                    new(alignHorizontal: ETextAlignHorizontal.RIGHT, alignVertical: ETextAlignVertical.CENTER));
                inputLine.AddChild(inputLabel, pageID);

                // TODO: Better way than to create a copy?
                InputField inputField = new(field.Name, field.Label, field.Placeholder, field.Model,
                    new(weight: _formAttributes.inputColumnWeight, margin: new(0, 0, _formAttributes.paddingBetweenColumns / 2.0f, 0)));
                newFields.Add(inputField);
                inputLine.AddChild(inputField, pageID);
            }
            _fields = newFields;

            // TODO: Add reset and submit buttons
        }

        /*
        private Dictionary<string, IInputFieldModel> Submit()
        {
            Dictionary<string, IInputFieldModel> models = new(_items.Count);
            foreach(var item in _items)
            {
                models.Add(item.Key, item.Value.GetModel());
            }
            return models;
        }
        */



        /*
        public List<IResult> ValidateFields()
        {
            List<IResult> results = new List<IResult>(_items.Count);
            foreach (IFormItem item in _items)
            {

            }
            return results;
        }
        */
    }
}
