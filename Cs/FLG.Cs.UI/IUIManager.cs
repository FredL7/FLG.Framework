using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Layouts;


namespace FLG.Cs.UI {
    public interface IUIManager : IServiceInstance {
        public IEnumerable<ILayout> GetLayouts();
    }
}
