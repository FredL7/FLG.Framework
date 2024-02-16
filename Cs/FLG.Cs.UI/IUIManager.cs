using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Layouts;
using FLG.Cs.UI.Pages;

namespace FLG.Cs.UI {
    public interface IUIManager : IServiceInstance {
        public IEnumerable<ILayout> GetLayouts();
    }
}
