using Automaton.DataAccesLayer.Repositories;
using Automaton.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Automaton.DataAccesLayer.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection Configure(this IServiceCollection services)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IPaymentReposotory, PaymentReposotory>();
            services.AddTransient<ISummaryRepository, SummaryRepository>();
            return services;
        }
    }
}
