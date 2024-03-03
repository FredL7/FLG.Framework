using FLG.Cs.IDatamodel;
using FLG.Cs.Math;

namespace FLG.Cs.UI {
    public class UIFactoryProxy : IUIFactory {
        public bool IsProxy() => true;
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered() { }

        // TMP
        public ILayoutElement ProxyLayoutElement(string name, LayoutAttributes attributes) { throw new NotImplementedException(); }

        #region Layouts
        public ILayoutElement HStack(string name, LayoutAttributes layoutAttr, GridAttributes gridAttr, bool isTarget) { throw new NotImplementedException(); }
        public ILayoutElement VStack(string name, LayoutAttributes layoutAttr, GridAttributes gridAttr, bool isTarget) { throw new NotImplementedException(); }
        #endregion Layouts

        #region Widgets
        public ILayoutElement Button(string name, string text, Action action, LayoutAttributes attributes) { throw new NotImplementedException(); }
        public ILayoutElement Label(string name, string text, LayoutAttributes attributes) { throw new NotImplementedException(); }
        public ILayoutElement Sprite(string name, string source, LayoutAttributes attributes) { throw new NotImplementedException(); }
        #endregion Widgets
    }
}
