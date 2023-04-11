using Automaton.Application.Email.EmailMessagePrepare;

namespace Automaton.Application.Email.Smtp
{
    public interface ISmtpEmailClient
    {
        void Send(SendEmailModel model);
    }
}
