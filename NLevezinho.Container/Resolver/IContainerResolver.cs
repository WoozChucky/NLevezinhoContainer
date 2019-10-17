using System;
using NLevezinho.Container.Registry;
using NLevezinho.Container.Util;

namespace NLevezinho.Container.Resolver
{
    public interface IContainerResolver : IHideObjectMembers
    {
        object GetService(IContainerRegistry registry, Type serviceType);
    }
}
