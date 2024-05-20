using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Grids;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Widgets;


// TODO: Could be static class, no need for instance
namespace FLG.Cs.UI
{
    public class UIFactory : IUIFactory {
        #region IServiceInstance
        public bool IsProxy() => false;
        public void OnServiceRegisteredFail() { Locator.Instance.Get<ILogManager>().Error("UI Factory Failed to register"); }
        public void OnServiceRegistered() { Locator.Instance.Get<ILogManager>().Debug("UI Factory Registered"); }
        #endregion IServiceInstance

        internal static AbstractLayoutElement? Xml(XmlNode node, string name)
        {
            var nodeType = node.Name;
            return nodeType switch
            {
                // Layouts
                "Container" => new Container(name, node),
                "HStack" => new HStack(name, node),
                "VStack" => new VStack(name, node),

                // Widgets
                "Button" => new Button(name, node), // Should throw in ctor (can't bind fct to click)
                "Label" => new Label(name, node),
                "Sprite" => new Sprite(name, node),
                "Text" => new Text(name, node),

                // Forms
                "Form" => new Form(name, node), // Should throw in ctor (specifics not implemented)
                "InputField" => new InputField(name, node),

                _ => null,
            };
        }

        #region Layouts
        public ILayoutElement Container(string name, LayoutAttributes attributes)
            => new Container(name, attributes);
        public ILayoutElement HStack(string name, LayoutAttributes layoutAttr, GridAttributes gridAttr)
            => new HStack(name, layoutAttr, gridAttr);

        public ILayoutElement VStack(string name, LayoutAttributes layoutAttr, GridAttributes gridAttr)
            => new VStack(name, layoutAttr, gridAttr);
        #endregion Layouts

        #region Widgets
        public IButton Button(string name, string source, Action action, LayoutAttributes attributes)
            => new Button(name, source, action, attributes);

        public ILabel Label(string name, string text, LayoutAttributes layoutAttr, TextAttributes textAttr)
            => new Label(name, text, layoutAttr, textAttr);

        public ISprite Sprite(string name, string source, LayoutAttributes attributes)
            => new Sprite(name, source, attributes);

        public IText Text(string name, string source, LayoutAttributes layoutAttr, TextAttributes textAttr)
            => new Text(name, source, layoutAttr, textAttr);
        #endregion Widgets

        #region Forms
        public IForm Form(string name, string title, List<IInputField> fields, Action submit, LayoutAttributes layoutAttr, FormAttributes formAttr)
            => new Form(name, title, fields, submit, layoutAttr, formAttr);

        public IInputField InputField(string name, string label, string placeholder, IInputFieldModel model, LayoutAttributes layoutAttr)
            => new InputField(name, label, placeholder, model, layoutAttr);
        // TODO: Second method of InputField without model that will use a default
        #endregion Forms
    }
}
