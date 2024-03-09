using FLG.Cs.IDatamodel;

namespace FLG.Cs.UI {
    public class UIManagerProxy : IUIManager {
        public bool IsProxy() => true;
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered() { }

        public void SetCurrentPage(string id) { }
        public IEnumerable<ILayout> GetLayouts() { return new List<ILayout>(); }
        public ILayout GetLayout(string name)
        {
            throw new NotImplementedException();
        }

        public void RegisterPage(IPage p) { }
        public void AddObserver(IUIObserver observer) { }
        public void RemoveObserver(IUIObserver observer) { }
    }
}
