using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NLevezinho.Container.Core;
using NLevezinho.Container.Registry;
using NLevezinho.Container.Util;

namespace NLevezinho.Container.Resolver
{
    public class SingletonServiceResolver : IServiceResolver
    {
        private readonly ConcurrentDictionary<Type, object> _singletonServices = new ConcurrentDictionary<Type, object>();
        
        public object Resolve(IRegistration registration, IContainerRegistry registry)
        {
            if (registration == null)
                throw new ArgumentNullException(nameof(registration));
            if (registry == null) 
                throw new ArgumentNullException(nameof(registry));

            if (registration.Type != null)
                if (_singletonServices.ContainsKey(registration.Type))
                {
                    return _singletonServices[registration.Type];
                }

            if (registration.Type.IsInterface && registration.Implementation != null)
            {
                // Interface and Implementation present
                if (_singletonServices.ContainsKey(registration.Implementation))
                {
                    return _singletonServices[registration.Implementation];
                }

                var implementation = Resolve(new Registration(registration.Implementation, 
                    Lifetime.Singleton), registry);

                _singletonServices.TryAdd(registration.Type, implementation);

                return implementation;
            }
            
            if (registration.Type.IsInterface && registration.Implementation == null)
            {
                // Interface is present, look for implementation in IContainerRegistry
                var subType = registry.GetSubTypeRegistered(registration.Type);

                if (subType != null)
                {
                    if (_singletonServices.ContainsKey(subType))
                    {
                        return _singletonServices[subType];
                    }

                    var implementation = Resolve(new Registration(subType, Lifetime.Singleton), registry);
                    
                    _singletonServices.TryAdd(registration.Type, implementation);

                    return implementation;
                }
                
                throw new Exception($"Could not infer implementation of type {registration.Type.FullName}.");
            }

            var serviceType = registration.Type;

            var constructor = registration.Type.GetValidConstructor();
                
            var requiredConstructorParams = constructor.GetRequiredParameters().ToList();
            
            if (requiredConstructorParams.Any(param => registry.Get(param) == null))
            {
                throw new Exception("Required constructor parameter not registered.");
            }

            var @params = new List<object>();
            foreach (var requiredConstructorParam in requiredConstructorParams)
            {
                if (_singletonServices.ContainsKey(requiredConstructorParam))
                {
                    @params.Add(_singletonServices[requiredConstructorParam]);
                }
                else
                {
                    var dependency = Resolve(new Registration(requiredConstructorParam, Lifetime.Singleton), registry);
                    @params.Add(dependency);
                }
            }

            if (@params.Any())
            {
                var requiredObject = Activator.CreateInstance(serviceType, @params.FirstOrDefault());

                _singletonServices.TryAdd(serviceType, requiredObject);

                return requiredObject;
            }
            else
            {
                var requiredObject = Activator.CreateInstance(serviceType);

                _singletonServices.TryAdd(serviceType, requiredObject);

                return requiredObject;
            }
        }
    }
}