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
                IServiceInstance potentialProxy = _services[typeof(T)];
                if (potentialProxy.IsProxy())
                {
                    _services[typeof(T)] = service;
                    service.OnServiceRegistered();
                }
                else
                {
                    service.OnServiceRegisteredFail();
                }
                return;
            }
            else
            {
                _services.Add(typeof(T), service);
                service.OnServiceRegistered();
            }
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
