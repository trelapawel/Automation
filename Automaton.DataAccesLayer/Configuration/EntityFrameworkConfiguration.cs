using Automaton.DataAccesLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Automaton.DataAccesLayer.Configuration
{
    public static class EntityFrameworkConfiguration
    {
        public static IServiceCollection Configure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AutomatonDbContext>(
                options =>
                {
                    options.UseSqlServer(connectionString);
                });
            return services;
        }
    }
}
