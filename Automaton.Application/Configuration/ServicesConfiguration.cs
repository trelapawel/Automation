using Automaton.Application.Email;
using Automaton.Application.Email.EmailMessagePrepare;
using Automaton.Application.Email.Imap;
using Automaton.Application.Email.Smtp;
using Automaton.Application.OrderPreparation;
using Automaton.Application.PaymentOrderMatching;
using Automaton.Application.PaymentPreparation;
using Automaton.Application.ProductPreparation;
using Microsoft.Extensions.DependencyInjection;

namespace Automaton.Application.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection Configure(this IServiceCollection services)
        {
            services.AddTransient<IEmailClientService, EmailClientService>();
            services.AddTransient<IImapClient, ImapClient>();
            services.AddTransient<ISmtpEmailClient, SmtpEmailClient>();
            services.AddTransient<IEmailMessagePreparator, EmailMessagePreparator>();
            services.AddTransient<IMessageBodyPreparator, MessageBodyPreparator>();

            services.AddTransient<IOrderPreparator, OrderPreparator>();
            services.AddTransient<IOrderProductGetter, OrderProductGetter>();
            services.AddTransient<IOrderPreparatorEmailAddressGetter, OrderPreparatorEmailAddressGetter>();
            services.AddTransient<INewOrdersFinder, NewOrdersFinder>();
            

            services.AddTransient<IProductPreparator, ProductPreparator>();
            services.AddTransient<IProductIdGetter, ProductIdGetter>();
            services.AddTransient<IProductNameGetter, ProductNameGetter>();
            services.AddTransient<IProductUrlGetter, ProductUrlGetter>();

            services.AddTransient<IPaymentPreparator, PaymentPreparator>();
            services.AddTransient<IPaymentAmountGetter, PaymentAmountGetter>();
            services.AddTransient<IPaymentTittleGetter, PaymentTittleGetter>();

            services.AddTransient<IPaymentOrderMatcher, PaymentOrderMatcher>();


            return services;
        }
    }
}
