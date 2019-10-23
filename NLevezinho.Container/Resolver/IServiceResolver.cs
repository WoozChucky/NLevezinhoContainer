using NLevezinho.Container.Core;
using NLevezinho.Container.Registry;

namespace NLevezinho.Container.Resolver
{
    public interface IServiceResolver
    {
        object Resolve(IRegistration registration, IContainerRegistry registry);
    }
}