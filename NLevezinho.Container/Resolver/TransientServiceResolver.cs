using System;
using System.Collections.Generic;
using System.Linq;
using NLevezinho.Container.Core;
using NLevezinho.Container.Registry;
using NLevezinho.Container.Util;

namespace NLevezinho.Container.Resolver
{
    public class TransientServiceResolver : IServiceResolver
    {
        public object Resolve(IRegistration registration, IContainerRegistry registry)
        {
            if (registration == null)
                throw new ArgumentNullException(nameof(registration));
            if (registry == null) 
                throw new ArgumentNullException(nameof(registry));

            if (registration.Type.IsInterface && registration.Implementation != null)
            {
                // Interface and Implementation present
                var implementation = Resolve(new Registration(registration.Implementation, 
                    Lifetime.Transient), registry);
                
                return implementation;
            }
            
            if (registration.Type.IsInterface && registration.Implementation == null)
            {
                // Interface is present, look for implementation in IContainerRegistry
                var subType = registry.GetSubTypeRegistered(registration.Type);

                if (subType != null)
                {
                    var implementation = Resolve(new Registration(subType, Lifetime.Transient), registry);

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
                var dependency = Resolve(new Registration(requiredConstructorParam, Lifetime.Singleton), registry); 
                @params.Add(dependency);
            }

            if (@params.Any())
            {
                var requiredObject = Activator.CreateInstance(serviceType, @params.FirstOrDefault());

                return requiredObject;
            }
            else
            {
                var requiredObject = Activator.CreateInstance(serviceType);
                
                return requiredObject;
            }
        }
    }
}