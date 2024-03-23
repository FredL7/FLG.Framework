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
                "Button" => new Button(name, node), // Should throw in ctor
                "Label" => new Label(name, node),
                "Sprite" => new Sprite(name, node),
                "Text" => new Text(name, node),

                // TMP
                "ProxyLayoutElement" => new ProxyLayoutElementLeaf(name, node),
                _ => null,
            };
        }

        public ILayoutElement ProxyLayoutElement(string name, LayoutAttributes attributes)
            => new ProxyLayoutElementLeaf(name, attributes);

        #region Layouts
        public ILayoutElement Container(string name, LayoutAttributes attributes, bool isTarget)
            => new Container(name, attributes, isTarget);
        public ILayoutElement HStack(string name, LayoutAttributes layoutAttr, GridAttributes gridAttr, bool isTarget)
            => new HStack(name, layoutAttr, gridAttr, isTarget);

        public ILayoutElement VStack(string name, LayoutAttributes layoutAttr, GridAttributes gridAttr, bool isTarget)
            => new VStack(name, layoutAttr, gridAttr, isTarget);
        #endregion Layouts

        #region Widgets
        public ILayoutElement Button(string name, string source, Action action, LayoutAttributes attributes)
            => new Button(name, source, action, attributes);

        public ILayoutElement Label(string name, string text, LayoutAttributes attributes)
            => new Label(name, text, attributes);

        public ILayoutElement Sprite(string name, string source, LayoutAttributes attributes)
            => new Sprite(name, source, attributes);

        public ILayoutElement Text(string name, string source, LayoutAttributes attributes)
            => new Text(name, source, attributes);
        #endregion Widgets
    }
}
