using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NLevezinho.Container.Util
{
    public static class ReflectionUtils
    {
        public static IEnumerable<Type> GetRequiredParameters(this ConstructorInfo constructor)
        {
            foreach (var parameter in constructor.GetParameters())
            {
                Console.WriteLine(
                    $"Parameter {parameter.Position} is named {parameter.Name} and is of type {parameter.ParameterType}");
            }

            return constructor.GetParameters().Select(p => p.ParameterType);
        }

        public static ConstructorInfo? GetValidConstructor(this Type type)
        {
            var constructors = type.GetConstructors().Where(ctor => ctor.IsPublic).ToList();
            
            if (constructors.Count > 1)
                throw new Exception($"The type {type.FullName} can have only one public constructor.");
            
            if (!constructors.Any())
                throw new Exception($"The type {type.FullName} has no public constructors registered.");
            
            return constructors.FirstOrDefault();
        }
    }
}