namespace FLG.Cs.IDatamodel {
    public interface IUIFactory : IServiceInstance {
        #region Layouts
        public ILayoutElement Container(string name, LayoutAttributes layoutAttr);
        public ILayoutElement HStack(string name, LayoutAttributes layoutAttr, GridAttributes gridAttr);
        public ILayoutElement VStack(string name, LayoutAttributes layoutAttr, GridAttributes gridAttr);
        #endregion Layouts

        #region Widgets
        public ILayoutElement Button(string name, string text, Action action, LayoutAttributes attributes);
        public ILayoutElement Label(string name, string text, LayoutAttributes layoutAttr, TextAttributes textAttr);
        public ILayoutElement Sprite(string name, string source, LayoutAttributes attributes);
        public ILayoutElement Text(string name, string text, LayoutAttributes layoutAttr, TextAttributes textAttr);
        #endregion Widgets
    }
}
