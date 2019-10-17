using System;
using Microsoft.Extensions.DependencyInjection;

namespace NLevezinho.Container
{
    public class NLContainerServiceProviderFactory : IServiceProviderFactory<INLContainer>
    {

        private readonly INLContainer _container;

        public NLContainerServiceProviderFactory()
        {
            _container = new NLContainer();
        }

        public INLContainer CreateBuilder(IServiceCollection services)
        {
            _container.RegisterFromServiceCollection(services);
            return _container;
        }

        public IServiceProvider CreateServiceProvider(INLContainer containerBuilder)
        {
            return _container;
        }
    }
}
