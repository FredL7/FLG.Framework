using FLG.Cs.Decorators;

namespace FLG.Cs.ServiceLocator {
    public class Locator : SingletonBase<Locator> {
        private readonly Dictionary<object, IServiceInstance> _services;

        private Locator()
        {
            _services = new();
        }

        public void Register<T>(T service) where T : IServiceInstance
        {
            if (_services.ContainsKey(typeof(T)))
            {
                if (service.IsProxy())
                {
                    _services[typeof(T)] = service;
                }
                else
                {
                    service.OnServiceRegisteredFail();
                }
                // TODO: attempt to upgrade into non-void
                return;
            }
            _services.Add(typeof(T), service);
            service.OnServiceRegistered();
        }

        public T Get<T>() where T : IServiceInstance
        {
            try
            {
                return (T)_services[typeof(T)];
            }
            catch (Exception)
            {
                throw new NotImplementedException($"Service not registered for type {typeof(T)}");
            }
        }

        /*public T? GetOptional<T>() where T : IServiceInstance
        {
            try
            {
                return (T)_services[typeof(T)];
            }
            catch (Exception)
            {
                return default;
            }
        }*/
    }
}
