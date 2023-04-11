using Automaton.Application.Email.EmailMessagePrepare.EmailTemplates;

namespace Automaton.Application.Email.EmailMessagePrepare
{
    public interface IMessageBodyPreparator
    {
        string PrepareOrderSummaryMessage(ITemplate template, OrderSummaryEmailTemplateModel model);
        string PrepareMessageWithOrderedProduct(ITemplate template, MessageWithOrderedProductModel model);
        string PrepareDailySummaryMessage(ITemplate template, DailySummaryTemplateModel model);
    }
}
