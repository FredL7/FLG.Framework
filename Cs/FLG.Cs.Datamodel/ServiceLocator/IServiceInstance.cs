namespace FLG.Cs.Datamodel {
    public interface IServiceInstance {
        public bool IsProxy();
        public void OnServiceRegisteredFail();
        public void OnServiceRegistered();
    }
}
