namespace Automaton.Application.Email.EmailMessagePrepare.EmailTemplates
{
    public class MessageWithOrderedProductModel
    {
        public string ProductName { get; set; } = string.Empty;
        public string OrderNo { get; set; } = string.Empty;
        public string LinkToProduct { get; set; } = string.Empty;
        public string PasswordToProduct { get; set; } = string.Empty;
    }
}
