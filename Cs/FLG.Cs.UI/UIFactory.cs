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
                "HStack" => new HStack(name, node),
                "VStack" => new VStack(name, node),
                "ProxyLayoutElement" => new ProxyLayoutElementLeaf(name, node),
                _ => null,
            };
        }

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
        public ILayoutElement ProxyLayoutElement(string name,
            float width, float height, Spacing margin, Spacing padding, int order, float weight, bool isTarget)
            => new ProxyLayoutElementLeaf(name, width, height, margin, padding, order, weight, isTarget);
        #endregion Widgets
    }
}
