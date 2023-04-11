using Automaton.Application.Email.EmailMessagePrepare;
using Automaton.Application.Email.Imap;
using Automaton.Application.Email.Smtp;

namespace Automaton.Application.Email
{
    public class EmailClientService : IEmailClientService
    {
        private readonly IImapClient _imapClient;
        private readonly ISmtpEmailClient _smtpClient;

        public EmailClientService(
            IImapClient imapClient,
            ISmtpEmailClient smtpEmailClient)
        {
            _imapClient = imapClient;
            _smtpClient = smtpEmailClient;
        }
        public IEnumerable<EmailMessage> GetNewOrderMessages()
        {
            return _imapClient.GetMessagesByGmailLabel(Labels.OrderNew);
        }
        public EmailClientResponse SetMessageStatusToNotPaid(IEnumerable<uint> uniqueMessageIds)
        {
            return _imapClient.SetLabel(uniqueMessageIds, new List<string>() { Labels.OrderNotPaid });
        }

        public EmailClientResponse SetMessageStatusToPaid(IEnumerable<uint> uniqueMessageIds)
        {
            return _imapClient.SetLabel(uniqueMessageIds, new List<string>() { Labels.OrderPaid });
        }

        public EmailClientResponse SetMessageStatusToDuplicate(IEnumerable<uint> uniqueMessageIds)
        {
            return _imapClient.SetLabel(uniqueMessageIds, new List<string>() { Labels.OrderDuplicate });
        }

        public IEnumerable<EmailMessage> GetNewProductMessages()
        {
            return _imapClient.GetMessagesByGmailLabel(Labels.NewProduct);
        }

        public EmailClientResponse SetMessageStatusToProductAdded(IEnumerable<uint> uniqueMessageIds)
        {
            return _imapClient.SetLabel(uniqueMessageIds, new List<string>() { Labels.ProductAdded });
        }

        public IEnumerable<EmailMessage> GetNewPaymentMessages()
        {
            return _imapClient.GetMessagesByGmailLabel(Labels.PaymentNew);
        }

        public EmailClientResponse SetMessageStatusToOrderAdded(IEnumerable<uint> uniqueMessageIds)
        {
            return _imapClient.SetLabel(uniqueMessageIds, new List<string>() { Labels.OrderAdded });
        }

        public EmailClientResponse SetMessageStatusToPaymentAdded(IEnumerable<uint> uniqueMessageIds)
        {
            return _imapClient.SetLabel(uniqueMessageIds, new List<string>() { Labels.PaymentAdded });
        }

        public void SendEmail(SendEmailModel model)
        {
            _smtpClient.Send(model);
        }

        public void SendEmails(IEnumerable<SendEmailModel> models)
        {
            foreach(var model in models)
            {
                SendEmail(model);
            }
        }

        public EmailClientResponse SetMessageStatusOrderSent(IEnumerable<uint> uniqueMessageIds)
        {
            return _imapClient.SetLabel(uniqueMessageIds, new List<string>() { Labels.OrderSent });
        }

    }
}
