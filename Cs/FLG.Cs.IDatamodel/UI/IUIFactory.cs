namespace FLG.Cs.IDatamodel {
    public interface IUIFactory : IServiceInstance {
        #region Layouts
        public ILayoutElement Container(string name, LayoutAttributes layoutAttr);
        public ILayoutElement HStack(string name, LayoutAttributes layoutAttr, GridAttributes gridAttr);
        public ILayoutElement VStack(string name, LayoutAttributes layoutAttr, GridAttributes gridAttr);
        #endregion Layouts

        #region Widgets
        public IButton Button(string name, string text, Action action, LayoutAttributes attributes);
        public ILabel Label(string name, string text, LayoutAttributes layoutAttr, TextAttributes textAttr);
        public ISprite Sprite(string name, string source, LayoutAttributes attributes);
        public IText Text(string name, string text, LayoutAttributes layoutAttr, TextAttributes textAttr);
        #endregion Widgets

        #region Forms
        public IForm Form(string name, List<IInputField> fields, Action submit, LayoutAttributes attributes);
        public IInputField InputField(string name, string label, string placeholder, IInputFieldModel model, LayoutAttributes layoutAttr);
        #endregion Forms
    }
}
