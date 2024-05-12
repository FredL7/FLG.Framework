using FLG.Cs.IDatamodel;
using FLG.Cs.UI.Grids;
using System.Xml;

/*
 * TODO: Maybe have a form factory that would build a form from a list of parameters
 * That way the form itself can controls its styling/UI
 * 
 * TODO: Use generic IFieldItem instead IInputField and IInputFieldModel could be renamed ot IFieldItemModel
 * 
 * TODO: Use tests to fix the fact that VStack "container" childrens (form fields) are not shown in Godot
 * And that "container" layout doesn't work
 */
namespace FLG.Cs.UI.Widgets {
    internal class Form : Container, IForm {
        public override ELayoutElement Type { get => ELayoutElement.FORM; }

        public FormModel Model { get; private set; }
        public Action SubmitAction { get; }

        private readonly List<IInputField> _fields;

        public Form(string name, XmlNode node) : base(name, node) {
            // Should not create Form from xml (unless we add a custom way to add childs so we can dynamically create the FormModel by using the default IInputFieldModel)
            throw new NotImplementedException();
        }

        public Form(string name, List<IInputField> fields, Action submitAction, LayoutAttributes attributes)
            : base(name, attributes)
        {
            Model = new FormModel(fields);
            SubmitAction = submitAction;
            _fields = fields;
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

            // TODO: Add header

            foreach (var field in _fields)
                container.AddChild(field, pageID);

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
