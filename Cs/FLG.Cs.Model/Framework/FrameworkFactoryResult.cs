using FLG.Cs.Datamodel;


namespace FLG.Cs.Model {
    public struct FrameworkFactoryResult<T> where T : IServiceInstance {
        public Result result;
        public T? manager;
    }
}
