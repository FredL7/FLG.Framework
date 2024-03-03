using System.Xml;

using FLG.Cs.IDatamodel;
using FLG.Cs.Math;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Grids;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Widgets;

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
                "HStack" => new HStack(name, node),
                "VStack" => new VStack(name, node),

                // Widgets
                "Button" => new Button(name, node), // Should throw in ctor
                "Label" => new Label(name, node),
                "Sprite" => new Sprite(name, node),

                // TMP
                "ProxyLayoutElement" => new ProxyLayoutElementLeaf(name, node),
                _ => null,
            };
        }

        public ILayoutElement ProxyLayoutElement(string name,
            float width, float height, Spacing margin, Spacing padding, int order, float weight)
            => new ProxyLayoutElementLeaf(name, width, height, margin, padding, order, weight);

        #region Layouts
        public ILayoutElement HStack(string name,
            float width, float height, Spacing margin, Spacing padding, int order, float weight, bool isTarget,
            EGridDirection direction, EGridJustify justify, EGridAlignment alignment)
                => new HStack(name, width, height, margin, padding, order, weight, isTarget, direction, justify, alignment);

        public ILayoutElement VStack(string name,
            float width, float height, Spacing margin, Spacing padding, int order, float weight, bool isTarget,
            EGridDirection direction, EGridJustify justify, EGridAlignment alignment)
                => new VStack(name, width, height, margin, padding, order, weight, isTarget, direction, justify, alignment);
        #endregion Layouts

        #region Widgets
        public ILayoutElement Button(string name, string source, Action action,
            float width, float height, Spacing margin, Spacing padding, int order, float weight)
            => new Button(name, source, action, width, height, margin, padding, order, weight);

        public ILayoutElement Label(string name, string text,
            float width, float height, Spacing margin, Spacing padding, int order, float weight)
            => new Label(name, text, width, height, margin, padding, order, weight);

        public ILayoutElement Sprite(string name, string source,
            float width, float height, Spacing margin, Spacing padding, int order, float weight)
            => new Sprite(name, source, width, height, margin, padding, order, weight);
        #endregion Widgets
    }
}
