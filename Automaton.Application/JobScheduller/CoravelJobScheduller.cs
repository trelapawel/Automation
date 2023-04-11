using Automaton.Application.Configuration.Appsettongs;
using Automaton.Application.Jobs;
using Coravel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Automaton.Application.JobScheduller
{
    public static class CoravelJobScheduller
    {
        public static IServiceProvider ScheduleCoravel(this IServiceProvider provider, ConfigurationManager configuration)
        {
            var settings = provider.GetService<IOptions<AutomatonSettings>>();
            string defaultCron = "*/5 * * * *";  //At every 5th minute
            string checkNewOrdersCron = string.IsNullOrEmpty(settings?.Value.CheckNewOrdersCron) ? defaultCron : settings.Value.CheckNewOrdersCron;
            string checkNewProductsCron = string.IsNullOrEmpty(settings?.Value.CheckNewProductsCron) ? defaultCron : settings.Value.CheckNewProductsCron;
            string checkNewPaymentsCron = string.IsNullOrEmpty(settings?.Value.CheckNewPaymentsCron) ? defaultCron : settings.Value.CheckNewPaymentsCron;
            string dailySummaryCron = string.IsNullOrEmpty(settings?.Value.DailySummaryCron) ? defaultCron : settings.Value.DailySummaryCron;

            provider.UseScheduler(scheduler =>
            {
                scheduler.Schedule<NewOrdersJob>().Cron(checkNewOrdersCron);
                scheduler.Schedule<NewProductsJob>().Cron(checkNewProductsCron);
                scheduler.Schedule<NewPaymentInformationJob>().Cron(checkNewPaymentsCron);
                scheduler.Schedule<SendDailySummaryJob>().Cron(dailySummaryCron);
            });
            return provider;
        }
    }
}
