using FLG.Cs.Datamodel.ServiceLocator;
using FLG.Cs.Datamodel.UI.Layouts;
using FLG.Cs.Datamodel.UI.Pages;


namespace FLG.Cs.Datamodel.UI {
    public interface IUIManager : IServiceInstance {
        public void SetCurrentPage(string id);
        public IEnumerable<ILayout> GetLayouts();
        public ILayout GetLayout(string name);
        public IPage GetPage(string id);
        public void AddObserver(IUIObserver observer);
        public void RemoveObserver(IUIObserver observer);
    }
}
