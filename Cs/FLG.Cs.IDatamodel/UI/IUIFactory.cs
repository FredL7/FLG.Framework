using FLG.Cs.Math;

namespace FLG.Cs.IDatamodel {
    public interface IUIFactory : IServiceInstance {

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
        public ILayoutElement ProxyLayoutElement(
            string name,
            float width = 0,
            float height = 0,
            Spacing margin = default,
            Spacing padding = default,
            int order = 0,
            float weight = 1f,
            bool isTarget = false); // TODO: Leaf element can't be target?
        #endregion Widgets
    }
}
