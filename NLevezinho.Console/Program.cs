using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using NLevezinho.Container;

namespace NLevezinho.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            INLContainer container = new NLContainer();
            container.Registry.RegisterTransient<IWorld, HelloWorld>();
            container.Registry.RegisterSingleton<IWorld, HelloWorld>();
            container.Registry.RegisterSingleton<Car>();

            var hello = container.GetService<IWorld>();
            
            System.Console.WriteLine(hello.GetHashCode());
            
            hello = container.GetService<IWorld>();
            
            System.Console.WriteLine(hello.GetHashCode());

            /*
            var world = container.GetService<IWorld>();
            var cls = container.GetService<Car>();

            cls.Update("Nuno");

            cls.Hi();
            
            var cars2 = container.GetService<Car>();
            
            cars2.Hi();
            
            cars2.Update("Levezinho");
            
            cars2.Hi();
            
            var cars3 = container.GetService<Car>();
            
            cars3.Hi();
            */
        }
    }
}
