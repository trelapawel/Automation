using Automaton.Application.Configuration.Appsettongs;
using Automaton.Application.Email.EmailMessagePrepare;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Automaton.Application.Email.Smtp
{
    public class SmtpEmailClient : ISmtpEmailClient
    {
        private readonly IOptions<EmailSettings> _settings;

        public SmtpEmailClient(
            IOptions<EmailSettings> settings)
        {
            _settings = settings;
        }

        public void Send(SendEmailModel model)
        {
            try
            {
                var message = new MimeMessage();
                message.To.Add(MailboxAddress.Parse(model.To));
                message.From.Add(MailboxAddress.Parse(_settings.Value.Smtp.From));
                var body = new BodyBuilder();
                message.Subject = model.Subject;
                body.HtmlBody = model.Body;
                message.Body = body.ToMessageBody();
                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(_settings.Value.Smtp.SmtpServerName, _settings.Value.Smtp.Port);
                    smtp.Authenticate(_settings.Value.Smtp.Login, _settings.Value.Smtp.Password);
                    smtp.Send(message);
                    smtp.Disconnect(true);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
