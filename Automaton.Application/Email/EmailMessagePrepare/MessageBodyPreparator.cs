using Automaton.Application.Email.EmailMessagePrepare.EmailTemplates;

namespace Automaton.Application.Email.EmailMessagePrepare
{
    public class MessageBodyPreparator : IMessageBodyPreparator
    {
        public MessageBodyPreparator()
        {
        }

        public string PrepareDailySummaryMessage(ITemplate template, DailySummaryTemplateModel model)
        {
            var message = template.ToString();
            message = message.Replace("$$AllPaidOrders$$", model.AllPaidOrders.ToString());
            message = message.Replace("$$ThisMonthPaidOrders$$", model.ThisMonthPaidOrders.ToString());
            message = message.Replace("$$LastMonthPaidOrders$$", model.LastMonthPaidOrders.ToString());
            message = message.Replace("$$Amount$$", model.Amount.ToString());
            return message;
        }

        public string PrepareMessageWithOrderedProduct(ITemplate template, MessageWithOrderedProductModel model)
        {
            var message = template.ToString();
            message = message.Replace("$$ProductName$$", model.ProductName);
            message = message.Replace("$$OrderNo$$", model.OrderNo);
            message = message.Replace("$$PasswordToProduct$$", model.PasswordToProduct);
            message = message.Replace("$$LinkToProduct$$", model.LinkToProduct);
            return message;
        }

        public string PrepareOrderSummaryMessage(ITemplate template, OrderSummaryEmailTemplateModel model)
        {
            var message = template.ToString();
            message = message.Replace("$$ProductName$$", model.ProductName);
            message = message.Replace("$$OrderNo$$", model.OrderNo);
            message = message.Replace("$$Price$$", model.Price);
            message = message.Replace("$$AcountNo$$", model.AccountNumber);
            return message;
        }
    }
}
