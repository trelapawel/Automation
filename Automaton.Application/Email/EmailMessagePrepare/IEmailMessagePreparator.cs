using Automaton.Domain.Models;

namespace Automaton.Application.Email.EmailMessagePrepare
{
    public interface IEmailMessagePreparator
    {
        IEnumerable<SendEmailModel> PrepareOrderSummaryMessages(IEnumerable<Order> orders);
        IEnumerable<SendEmailModel> PrepareMessagesWithOrderedProduct(IEnumerable<Order> orders);
        SendEmailModel PrepareDailySummaryMessage(Summary summary);
    }
}
