using System;
using Microsoft.Extensions.DependencyInjection;
using NLevezinho.Container.Registry;
using NLevezinho.Container.Util;

namespace NLevezinho.Container
{
    public interface INLContainer : IServiceProvider, IHideObjectMembers
    {
        IContainerRegistry Registry { get; }

        void RegisterFromServiceCollection(IServiceCollection services);
    }
}
