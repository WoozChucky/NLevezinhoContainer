using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using NLevezinho.Container.Core;
using NLevezinho.Container.Registry;
using NLevezinho.Container.Util;

namespace NLevezinho.Container.Resolver
{
    internal class DefaultContainerResolver : IContainerResolver
    {
        private readonly ConcurrentDictionary<Lifetime, IServiceResolver> _resolvers =
            new ConcurrentDictionary<Lifetime, IServiceResolver>();

        internal DefaultContainerResolver()
        {
            _resolvers.TryAdd(Lifetime.Singleton, new SingletonServiceResolver());
            _resolvers.TryAdd(Lifetime.Transient, new TransientServiceResolver());
        }
        
        public object GetService(IContainerRegistry registry, Type serviceType)
        {
            
            if (!registry.Registered(serviceType))
                throw new Exception($"Type {serviceType.FullName} is not registered.");
            
            var registration = registry.Get(serviceType);

            return _resolvers[registration.Lifetime].Resolve(registration, registry);
        }
    }
}

/*
 *
    var nameExpression = Expression.Parameter(typeof(string), "name");
    var intExpression = Expression.Parameter(typeof(int), "age");

    var parameterExpressionList = new List<Expression> {nameExpression, intExpression};

    var expression = Expression.New(constructorInfo, parameterExpressionList);

    var lambda = Expression.Lambda(serviceType, expression, parameters: new []{nameExpression, intExpression});

    var compiled = lambda.Compile().DynamicInvoke("Nuno", 25);

    if (compiled != null)
    {

    }
 * 
 */
