namespace FLG.Cs.IDatamodel {
    public interface IServiceInstance {
        public bool IsProxy();
        public void OnServiceRegisteredFail();
        public void OnServiceRegistered();
    }
}
