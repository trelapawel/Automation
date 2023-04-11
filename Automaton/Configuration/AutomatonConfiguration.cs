using Automaton.Application.Configuration;
using Automaton.Application.Configuration.Appsettongs;

namespace Automaton.Configuration
{
    public static class AutomatonConfiguration
    {
        public static IServiceCollection Configure(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.Configure<AutomatonSettings>(configuration.GetSection("Automaton"));
            services.Configure<EmailSettings>(configuration.GetSection("Email"));
            AutomatonApplicationConfiguration.Configure(services, connectionString);
            return services;
        }
    }
}
