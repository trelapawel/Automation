using Automaton.Application.Configuration.Appsettongs;
using Automaton.Application.Email.EmailMessagePrepare.EmailTemplates;
using Automaton.Domain.Models;
using Automaton.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace Automaton.Application.Email.EmailMessagePrepare
{
    public class EmailMessagePreparator : IEmailMessagePreparator
    {
        private readonly IProductRepository _productRepository;
        private readonly IMessageBodyPreparator _messageBodyPreparator;
        private readonly IOptions<AutomatonSettings> _settings;

        public EmailMessagePreparator(
            IProductRepository productRepository,
            IMessageBodyPreparator messageBodyPreparator,
            IOptions<AutomatonSettings> settings)
        {
            _productRepository = productRepository;
            _messageBodyPreparator = messageBodyPreparator;
            _settings = settings;
        }

        public IEnumerable<SendEmailModel> PrepareOrderSummaryMessages(IEnumerable<Order> orders)
        {
            List<SendEmailModel> sendEmailModels = new();
            foreach(var order in orders)
            {
                sendEmailModels.Add(PrepareOrderSummaryMessage(order));
            }
            return sendEmailModels;
        }

        public IEnumerable<SendEmailModel> PrepareMessagesWithOrderedProduct(IEnumerable<Order> orders)
        {
            List<SendEmailModel> sendEmailModels = new();
            foreach (var order in orders)
            {
                sendEmailModels.Add(PrepareMessageWithOrderedProduct(order));
            }
            return sendEmailModels;
        }

        public SendEmailModel PrepareOrderSummaryMessage(Order order)
        {
            var product = _productRepository.GetById(order.ProductId);
            var model = new OrderSummaryEmailTemplateModel
            {
                AccountNumber = _settings.Value.AccountNumber,
                OrderNo = order.Id.ToString(),
                Price = order.Price.ToString(),
                ProductName = product.Name
            };
            return new SendEmailModel
            {
                To = order.CustomerEmailAddress,
                Subject = $"[Iguana do wycinania] Podsumowanie zamówienia nr {order.Id}.",
                Body = _messageBodyPreparator.PrepareOrderSummaryMessage(new OrderSummaryEmailTemplate(), model)
            };
        }

        public SendEmailModel PrepareMessageWithOrderedProduct(Order order)
        {
            var product = _productRepository.GetById(order.ProductId);
            var model = new MessageWithOrderedProductModel
            {
                LinkToProduct = product.UrlToFile,
                OrderNo = order.Id.ToString(),
                PasswordToProduct = _settings.Value.FilePassword,
                ProductName = product.Name
            };
            return new SendEmailModel
            {
                To = order.CustomerEmailAddress,
                Subject = $"[Iguana do wycinania] Zamówiony produkt nr zamówienia {order.Id}.",
                Body = _messageBodyPreparator.PrepareMessageWithOrderedProduct(new MessageWithOrderedProductTemplate(), model)
            };
        }

        public SendEmailModel PrepareDailySummaryMessage(Summary summary)
        {
            var model = new DailySummaryTemplateModel
            {
                AllPaidOrders = summary.AllPaidOrders,
                ThisMonthPaidOrders = summary.ThisMonthPaidOrders,
                LastMonthPaidOrders = summary.LastMonthPaidOrders,
                Amount = summary.Amount
            };
            var emailTo = _settings.Value.DailySummaryEmailAddress;
            if (string.IsNullOrEmpty(emailTo))
            {
                return null;
            }
            return new SendEmailModel
            {
                To = emailTo,
                Subject = "[Iguana do wycinania] Podsumowanie",
                Body = _messageBodyPreparator.PrepareDailySummaryMessage(new DailySummaryTemplate(), model)
            };
        }
    }
}
