namespace Automaton.Application.Email.EmailMessagePrepare.EmailTemplates
{
    public class OrderSummaryEmailTemplateModel
    {
        public string ProductName { get; set; } = string.Empty;
        public string OrderNo { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
    }
}
