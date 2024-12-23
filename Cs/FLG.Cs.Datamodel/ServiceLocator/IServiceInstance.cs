namespace FLG.Cs.Datamodel.ServiceLocator {
    public interface IServiceInstance {
        public void OnServiceRegisteredFail();
        public void OnServiceRegistered();
    }
}
