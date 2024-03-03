namespace FLG.Cs.IDatamodel {
    public interface IUIFactory : IServiceInstance {
        // TMP
        public ILayoutElement ProxyLayoutElement(string name, LayoutAttributes attributes);

        #region Layouts
        public ILayoutElement HStack(string name, LayoutAttributes layoutAttr, GridAttributes gridAttr, bool isTarget = false);
        public ILayoutElement VStack(string name, LayoutAttributes layoutAttr, GridAttributes gridAttr, bool isTarget = false);
        #endregion Layouts

        #region Widgets
        public ILayoutElement Button(string name, string text, Action action, LayoutAttributes attributes);
        public ILayoutElement Label(string name, string text, LayoutAttributes attributes);
        public ILayoutElement Sprite(string name, string source, LayoutAttributes attributes);
        #endregion Widgets
    }
}
