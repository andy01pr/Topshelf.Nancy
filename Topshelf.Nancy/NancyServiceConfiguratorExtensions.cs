using System;
using Topshelf.ServiceConfigurators;
using Topshelf.HostConfigurators;

namespace Topshelf.Nancy
{
    public static class NancyServiceConfiguratorExtensions
    {
        public static ServiceConfigurator<T> WithNancyEndpoint<T>(this ServiceConfigurator<T> configurator, HostConfigurator hostconfigurator, Action<NancyServiceConfiguration> nancyConfigurator) where T : class
        {
            var nancyServiceConfiguration = new NancyServiceConfiguration();

            nancyConfigurator(nancyServiceConfiguration);

            var nancyService = new NancyService();

            nancyService.Configure(nancyServiceConfiguration);

            configurator.AfterStartingService(_ => nancyService.Start());

            configurator.BeforeStoppingService(_ => nancyService.Stop());

            hostconfigurator.BeforeInstall(_ => nancyService.BeforeInstall());

            hostconfigurator.BeforeUninstall(nancyService.BeforeUninstall);

            return configurator;
        }
    }
}