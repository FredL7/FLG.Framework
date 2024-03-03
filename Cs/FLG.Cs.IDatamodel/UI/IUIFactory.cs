using FLG.Cs.Math;

namespace FLG.Cs.IDatamodel {
    public interface IUIFactory : IServiceInstance {
        // TMP
        public ILayoutElement ProxyLayoutElement(
            string name,
            float width = 0,
            float height = 0,
            Spacing margin = default,
            Spacing padding = default,
            int order = 0,
            float weight = 1f);

        #region Layouts
        public ILayoutElement HStack(
            string name,
            float width = 0,
            float height = 0,
            Spacing margin = default,
            Spacing padding = default,
            int order = 0,
            float weight = 1f,
            bool isTarget = false,
            EGridDirection direction = EGridDirection.NORMAL,
            EGridJustify justify = EGridJustify.START,
            EGridAlignment alignment = EGridAlignment.START);

        public ILayoutElement VStack(
            string name,
            float width = 0,
            float height = 0,
            Spacing margin = default,
            Spacing padding = default,
            int order = 0,
            float weight = 1f,
            bool isTarget = false,
            EGridDirection direction = EGridDirection.NORMAL,
            EGridJustify justify = EGridJustify.START,
            EGridAlignment alignment = EGridAlignment.START);
        #endregion Layouts

        #region Widgets
        public ILayoutElement Button(
            string name,
            string text,
            Action action,
            float width = 0,
            float height = 0,
            Spacing margin = default,
            Spacing padding = default,
            int order = 0,
            float weight = 1f);

        public ILayoutElement Label(
            string name,
            string text,
            float width = 0,
            float height = 0,
            Spacing margin = default,
            Spacing padding = default,
            int order = 0,
            float weight = 1f);

        public ILayoutElement Sprite(
            string name,
            string source,
            float width = 0,
            float height = 0,
            Spacing margin = default,
            Spacing padding = default,
            int order = 0,
            float weight = 1f);
        #endregion Widgets
    }
}
