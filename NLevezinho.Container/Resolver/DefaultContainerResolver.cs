using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using NLevezinho.Container.Registry;

namespace NLevezinho.Container.Resolver
{
    internal class DefaultContainerResolver : IContainerResolver
    {
        public object GetService(IContainerRegistry registry, Type serviceType)
        {

            if (!registry.Registered(serviceType))
                throw new Exception($"Type {serviceType.FullName} is not registered.");


            var registration = registry.Get(serviceType);

            if (registration.Implementation == null)
            {
                var constructor = registration.Type.GetConstructors();

                foreach (var constructorInfo in constructor)
                {
                    if (constructorInfo.IsPublic)
                    {
                        foreach (var param in constructorInfo.GetParameters())
                        {
                            Console.WriteLine(
                                $"Parameter {param.Position} is named {param.Name} and is of type {param.ParameterType}");
                        }

                        var nameExpression = Expression.Parameter(typeof(string));
                        var intExpression = Expression.Parameter(typeof(int));

                        var parameterExpressionList = new List<Expression> {nameExpression, intExpression};

                        var expression = Expression.New(constructorInfo, parameterExpressionList);

                        var lambda = Expression.Lambda(serviceType, expression, parameters: new []{nameExpression, intExpression});

                        var compiled = lambda.Compile().DynamicInvoke("Nuno", 25);

                        if (compiled != null)
                        {

                        }

                        
                        //Activator.CreateInstance(serviceType, 1, "");
                    }
                }
            }
            else
            {
                
            }

            return null;
        }
    }
}
