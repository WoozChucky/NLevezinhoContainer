using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NLevezinho.Container.Core;

namespace NLevezinho.Container.Registry
{
    internal class DefaultContainerRegistry : IContainerRegistry
    {
        private readonly ConcurrentBag<IRegistration> _registrations;

        internal DefaultContainerRegistry()
        {
            _registrations = new ConcurrentBag<IRegistration>();
        }

        public void Register<TProvider, TImplementation>(ServiceLifetime lifetime)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    RegisterSingleton<TProvider, TImplementation>();
                    break;
                case ServiceLifetime.Scoped:
                    RegisterScoped<TProvider, TImplementation>();
                    break;

                case ServiceLifetime.Transient:
                    RegisterTransient<TProvider, TImplementation>();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }

        public void Register<TImplementation>(ServiceLifetime lifetime)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    RegisterSingleton<TImplementation>();
                    break;
                case ServiceLifetime.Scoped:
                    RegisterScoped<TImplementation>();
                    break;

                case ServiceLifetime.Transient:
                    RegisterTransient<TImplementation>();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }

        #region Singleton Registration

        public void RegisterSingleton<TProvider, TImplementation>()
        {
            Register<TProvider, TImplementation>(Lifetime.Singleton);
        }

        public void RegisterSingleton(Type provider, Type implementation)
        {
            Register(provider, implementation, Lifetime.Singleton);
        }

        public void RegisterSingleton<TImplementation>()
        {
            Register<TImplementation>(Lifetime.Singleton);
        }

        public void RegisterSingleton(Type provider)
        {
            Register(provider, Lifetime.Singleton);
        }

        #endregion
        
        #region Scoped Registration

        public void RegisterScoped<TProvider, TImplementation>()
        {
            Register<TProvider, TImplementation>(Lifetime.Scoped);
        }

        public void RegisterScoped(Type provider, Type implementation)
        {
            Register(provider, implementation, Lifetime.Scoped);
        }

        public void RegisterScoped<TImplementation>()
        {
            Register<TImplementation>(Lifetime.Scoped);
        }

        public void RegisterScoped(Type provider)
        {
            Register(provider, Lifetime.Scoped);
        }

        #endregion

        #region Transient Registration

        public void RegisterTransient<TProvider, TImplementation>()
        {
            Register<TProvider, TImplementation>(Lifetime.Transient);
        }

        public void RegisterTransient<TImplementation>()
        {
            Register<TImplementation>(Lifetime.Transient);
        }

        public void RegisterTransient(Type provider, Type implementation)
        {
            Register(provider, implementation, Lifetime.Transient);
        }

        public void RegisterTransient(Type implementation)
        {
            Register(implementation, Lifetime.Transient);
        }

        #endregion


        public bool Registered(Type type)
        {
            foreach (var registration in _registrations)
            {
                if (registration.Type == type) 
                    return true;

                if (registration.Implementation != null && registration.Implementation == type) 
                    return true;
            }

            return false;
        }

        public Type GetSubTypeRegistered(Type baseType)
        {
            var type = _registrations.FirstOrDefault(reg => baseType.IsAssignableFrom(reg.Type) && reg.Type != baseType);
            if (type != null)
                return type.Type;
            
            type = _registrations.FirstOrDefault(reg => reg.Implementation != null
                                                        && baseType.IsAssignableFrom(reg.Implementation));
            return type?.Implementation;
        }

        public IRegistration Get(Type type)
        {
            foreach (var registration in _registrations)
            {
                if (registration.Type == type) return registration;
                if (registration.Implementation != null && registration.Implementation == type) return registration;
            }

            return null;
        }

        #region Private Helper Methods

        private void Register<TProvider, TImplementation>(Lifetime lifetime)
        {
            Register(typeof(TProvider), typeof(TImplementation), lifetime);
        }

        private void Register<TImplementation>(Lifetime lifetime)
        {
            Register(typeof(TImplementation), lifetime);
        }

        private void Register(Type provider, Lifetime lifetime)
        {
            //if (Registered(provider))
            //    throw new Exception($"Type {provider.FullName} is already registered.");

            _registrations.Add(new Registration(provider, lifetime));
        }

        private void Register(Type provider, Type implementation, Lifetime lifetime)
        {
            //if (Registered(provider) || Registered(implementation))
            //    throw new Exception($"Type {provider.FullName} is already registered.");

            _registrations.Add(new Registration(provider, implementation, lifetime));
        }

        #endregion

        public void Dispose()
        {
            
        }
    }
}
