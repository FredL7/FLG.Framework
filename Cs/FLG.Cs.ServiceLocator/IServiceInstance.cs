namespace FLG.Cs.ServiceLocator {
    public interface IServiceInstance {
        public bool IsProxy();
        public void OnServiceRegisteredFail();
        public void OnServiceRegistered();
    }
}
