using FLG.Cs.Datamodel;
using FLG.Cs.UI.Grids;
using System.Xml;

namespace FLG.Cs.UI.Widgets {
    internal class Form : Container, IForm {
        public override ELayoutElement Type { get => ELayoutElement.FORM; }

        public string Title { get; private set; }
        public FormModel Model { get; private set; }

        private List<IInputField> _fields;
        private readonly FormAttributes _formAttributes;
        private readonly Action<string, FormModel> _submitAction;

        public Form(string name, XmlNode node) : base(name, node)
        {
            // Should not create Form from xml (unless we add a custom way to add childs so we can dynamically create the FormModel by using the default IInputFieldModel)
            throw new NotImplementedException();
        }

        public Form(string name, string title, List<IInputField> fields, Action<string, FormModel> submitAction, LayoutAttributes layoutAttr, FormAttributes formAttr)
            : base(name, layoutAttr)
        {
            Title = title;
            Model = new FormModel(fields);
            _submitAction = submitAction;
            _fields = fields; // TODO: Better repalce (can't change layout values once ctor, so need to replace because of internal values)
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

            // Fields
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

            // Controls
            HStack controls = new(Name + "-controls", new(margin: new(0, 20, 0, 0)), new(justify: EGridJustify.CENTER));
            container.AddChild(controls, pageID);

            Button resetBtn = new(Name + "-control-reset", "Reset", ResetFields, new(margin: new(_formAttributes.paddingBetweenColumns / 2.0f, 0, 0, 0)));
            controls.AddChild(resetBtn, pageID);
            Button submitBtn = new(Name + "-control-submit", "Submit", SubmitForm, new(margin: new(0, 0, _formAttributes.paddingBetweenColumns / 2.0f, 0)));
            controls.AddChild(submitBtn, pageID);
        }

        private void ResetFields()
        {
            foreach (var field in _fields)
            {
                field.Model.Clear();
            }
        }

        private void SubmitForm()
        {
            if (ValidateFields())
            {
                _submitAction(Name, Model);
            }

            // TODO: Clear the form?
        }

        public bool ValidateFields()
        {
            // TODO
            return true;
        }
    }
}
