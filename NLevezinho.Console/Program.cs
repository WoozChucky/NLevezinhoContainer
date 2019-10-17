using System;
using Microsoft.Extensions.DependencyInjection;
using NLevezinho.Container;

namespace NLevezinho.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            INLContainer container = new NLContainer();
            container.Registry.RegisterSingleton<HelloWorld>();

            var cls = container.GetService<HelloWorld>();
            if (cls != null)
            {

            }
        }
    }
}
