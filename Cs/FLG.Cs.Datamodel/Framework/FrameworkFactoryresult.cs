using FLG.Cs.Datamodel.ServiceLocator;
using FLG.Cs.Datamodel.Validation;


namespace FLG.Cs.Datamodel.Framework {
    public struct FrameworkFactoryResult<T> where T : IServiceInstance {
        public Result result;
        public T? manager;
    }
}
