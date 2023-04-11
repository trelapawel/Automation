using Automaton.Application.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace Automaton.Application.Configuration
{
    public static class JobsConfiguration
    {
        public static IServiceCollection Configure(this IServiceCollection services)
        {
            services.AddTransient<NewOrdersJob>();
            services.AddTransient<NewProductsJob>();
            services.AddTransient<SendOrderSummaryJob>();
            services.AddTransient<NewPaymentInformationJob>();
            services.AddTransient<MatchOrdersWithPaymentsJob>();
            services.AddTransient<SendOrderedProductJob>();
            services.AddTransient<SendDailySummaryJob>();
            return services;
        }
    }
}
