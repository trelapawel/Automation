using Automaton.Application.Configuration.Appsettongs;
using Automaton.Application.Email.Exceptions;
using MailKit;
using MailKit.Search;
using Microsoft.Extensions.Options;

namespace Automaton.Application.Email.Imap
{
    public class ImapClient : IImapClient
    {
        private readonly IOptions<EmailSettings> _settings;

        public ImapClient(
            IOptions<EmailSettings> settings)
        {
            _settings = settings;
        }

        public IEnumerable<EmailMessage> GetAllMessages()
        {
            try
            {
                using (var client = new MailKit.Net.Imap.ImapClient())
                {
                    client.Connect(_settings.Value.Imap.ImapServerName, _settings.Value.Imap.Port, _settings.Value.Imap.Ssl);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_settings.Value.Imap.Login, _settings.Value.Imap.Password);
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);
                    var results = inbox.Search(SearchOptions.All, SearchQuery.All);
                    var messages = new List<EmailMessage>();
                    foreach (var uniqueId in results.UniqueIds)
                    {
                        var message = inbox.GetMessage(uniqueId);
                        messages.Add(new EmailMessage
                        {
                            Id = uniqueId.Id,
                            From = message.From.ToString(),
                            To = message.To.ToString(),
                            Body = message.HtmlBody,
                            RecivedAt = message.Date.UtcDateTime,
                            Subject = message.Subject
                        });
                    }
                    client.Disconnect(true);
                    return messages;
                }
            }
            catch(Exception ex)
            {
                throw new ImapClientException("Error while getting all messages", ex);
            }
        }

        public IEnumerable<EmailMessage> GetMessagesByGmailLabel(string label)
        {
            try
            {
                using (var client = new MailKit.Net.Imap.ImapClient())
                {
                    client.Connect(_settings.Value.Imap.ImapServerName, _settings.Value.Imap.Port, _settings.Value.Imap.Ssl);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_settings.Value.Imap.Login, _settings.Value.Imap.Password);
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);
                    var results = inbox.Search(SearchOptions.All, SearchQuery.HasGMailLabel(label));
                    var messages = new List<EmailMessage>();
                    foreach (var uniqueId in results.UniqueIds)
                    {
                        var message = inbox.GetMessage(uniqueId);
                        messages.Add(new EmailMessage
                        {
                            Id = uniqueId.Id,
                            From = message.From.ToString(),
                            To = message.To.ToString(),
                            Body = message.HtmlBody,
                            RecivedAt = message.Date.UtcDateTime,
                            Subject = message.Subject
                        });
                    }
                    client.Disconnect(true);
                    return messages;
                }
            }
            catch (Exception ex)
            {
                throw new ImapClientException($"Error while getting messages with label: {label}", ex);
            }
        }

        public EmailClientResponse SetLabel(IEnumerable<uint> uniqueIds, IList<string> labels)
        {
            try
            {
                if (labels == null)
                {
                    return EmailClientResponse.Failure(new string[] { "Error while changing message labels, list of labels is empty" });
                }
                if (!uniqueIds.Any())
                {
                    return EmailClientResponse.Failure(new string[] { "No messages to update" });
                }
                using (var client = new MailKit.Net.Imap.ImapClient())
                {
                    client.Connect(_settings.Value.Imap.ImapServerName, _settings.Value.Imap.Port, _settings.Value.Imap.Ssl);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_settings.Value.Imap.Login, _settings.Value.Imap.Password);
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadWrite);

                    foreach (var id in uniqueIds)
                    {
                        var uniqueId = new UniqueId(id);
                        inbox.SetLabels(uniqueId, labels, true);
                    }

                    client.Disconnect(true);
                    return EmailClientResponse.Success();
                }
            }
            catch(Exception ex)
            {
                return EmailClientResponse.Failure(ex.Message);
            }

        }
    }
}
