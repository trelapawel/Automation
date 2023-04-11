using Automaton.Application.Email.EmailMessagePrepare;

namespace Automaton.Application.Email
{ 
    public interface IEmailClientService
    {
        IEnumerable<EmailMessage> GetNewOrderMessages();
        IEnumerable<EmailMessage> GetNewProductMessages();
        IEnumerable<EmailMessage> GetNewPaymentMessages();
        EmailClientResponse SetMessageStatusToOrderAdded(IEnumerable<uint> uniqueMessageIds);
        EmailClientResponse SetMessageStatusToProductAdded(IEnumerable<uint> uniqueMessageIds);
        EmailClientResponse SetMessageStatusToPaymentAdded(IEnumerable<uint> uniqueMessageIds);
        EmailClientResponse SetMessageStatusToNotPaid(IEnumerable<uint> uniqueMessageIds);
        EmailClientResponse SetMessageStatusOrderSent(IEnumerable<uint> uniqueMessageIds);
        EmailClientResponse SetMessageStatusToPaid(IEnumerable<uint> uniqueMessageIds);
        EmailClientResponse SetMessageStatusToDuplicate(IEnumerable<uint> uniqueMessageIds);
        void SendEmails(IEnumerable<SendEmailModel> models);
        void SendEmail(SendEmailModel model);
    }
}
