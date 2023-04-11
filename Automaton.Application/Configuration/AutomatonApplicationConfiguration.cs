using Automaton.DataAccesLayer.Configuration;
using Coravel;
using Microsoft.Extensions.DependencyInjection;

namespace Automaton.Application.Configuration
{
    public static class AutomatonApplicationConfiguration
    {
        public static IServiceCollection Configure(this IServiceCollection services, string connectionSting)
        {
            services.AddScheduler();
            services.AddQueue();
            ServicesConfiguration.Configure(services);
            JobsConfiguration.Configure(services);
            DataAccessLayerConfiguration.Configure(services, connectionSting);
            return services;
        }
    }
}
