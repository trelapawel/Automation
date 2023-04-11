namespace Automaton.Application.Email.Imap
{
    public interface IImapClient
    {
        public IEnumerable<EmailMessage> GetAllMessages();
        IEnumerable<EmailMessage> GetMessagesByGmailLabel(string label);
        EmailClientResponse SetLabel(IEnumerable<uint> uniqueIds, IList<string> labels);
    }
}
