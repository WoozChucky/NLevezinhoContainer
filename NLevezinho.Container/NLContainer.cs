using System;
using Microsoft.Extensions.DependencyInjection;
using NLevezinho.Container.Registry;
using NLevezinho.Container.Resolver;

namespace NLevezinho.Container
{
    public class NLContainer : INLContainer
    {
        private readonly IContainerResolver _resolver;

        public NLContainer()
        {
            _resolver = new DefaultContainerResolver();
            Registry = new DefaultContainerRegistry();
        }

        public IContainerRegistry Registry { get; }

        public void RegisterFromServiceCollection(IServiceCollection services)
        {
            foreach (var service in services)
            {
                switch (service.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        if (service.ImplementationType != null)
                        {
                            Registry.RegisterSingleton(service.ServiceType, service.ImplementationType);
                        }
                        else
                        {
                            Registry.RegisterSingleton(service.ServiceType);
                        }
                        break;
                    case ServiceLifetime.Scoped:
                        if (service.ImplementationType != null)
                        {
                            Registry.RegisterScoped(service.ServiceType, service.ImplementationType);
                        }
                        else
                        {
                            Registry.RegisterScoped(service.ServiceType);
                        }
                        break;
                    case ServiceLifetime.Transient:
                        if (service.ImplementationType != null)
                        {
                            Registry.RegisterTransient(service.ServiceType, service.ImplementationType);
                        }
                        else
                        {
                            Registry.RegisterTransient(service.ServiceType);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public object GetService(Type serviceType)
        {
            return _resolver.GetService(Registry, serviceType);
        }
    }
}
