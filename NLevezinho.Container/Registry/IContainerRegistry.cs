using System;
using Microsoft.Extensions.DependencyInjection;
using NLevezinho.Container.Core;
using NLevezinho.Container.Util;

namespace NLevezinho.Container.Registry
{
    public interface IContainerRegistry : IDisposable, IHideObjectMembers
    {
        void Register<TProvider, TImplementation>(ServiceLifetime lifetime);
        void Register<TImplementation>(ServiceLifetime lifetime);

        void RegisterSingleton<TProvider, TImplementation>();
        void RegisterSingleton(Type provider, Type implementation);
        void RegisterSingleton<TImplementation>();
        void RegisterSingleton(Type provider);

        void RegisterScoped<TProvider, TImplementation>();
        void RegisterScoped(Type provider, Type implementation);
        void RegisterScoped<TImplementation>();
        void RegisterScoped(Type provider);

        void RegisterTransient<TProvider, TImplementation>();
        void RegisterTransient(Type provider, Type implementation);
        void RegisterTransient<TImplementation>();
        void RegisterTransient(Type implementation);

        bool Registered(Type type);
        Type GetSubTypeRegistered(Type baseType);

        IRegistration Get(Type type);
    }
}
