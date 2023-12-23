namespace FLG.Cs.ServiceLocator {
    public interface IServiceInstance {
        public bool IsProxy();
        public void OnServiceRegistered();
        public void OnServiceRegisteredFail();
    }
}
