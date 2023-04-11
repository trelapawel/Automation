using Automaton.DataAccesLayer.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Automaton.DataAccesLayer.Configuration
{
    public static class DataAccessLayerConfiguration
    {
        public static IServiceCollection Configure(this IServiceCollection services, string connectionString)
        {
            EntityFrameworkConfiguration.Configure(services, connectionString);
            RepositoriesConfiguration.Configure(services);
            services.AddTransient<IAutomatonDbContext, AutomatonDbContext>();

            return services;
        }
    }
}
