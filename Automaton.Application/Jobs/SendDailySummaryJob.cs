using Automaton.Application.Email;
using Automaton.Application.Email.EmailMessagePrepare;
using Automaton.Domain.Repositories;
using Coravel.Invocable;

namespace Automaton.Application.Jobs
{
    public class SendDailySummaryJob : IInvocable
    {
        private readonly ISummaryRepository _summaryRepository;
        private readonly IEmailMessagePreparator _emailMessagePreparator;
        private readonly IEmailClientService _emailClientService;

        public SendDailySummaryJob(
            ISummaryRepository summaryRepository,
            IEmailMessagePreparator emailMessagePreparator,
            IEmailClientService emailClientService)
        {
            _summaryRepository = summaryRepository;
            _emailMessagePreparator = emailMessagePreparator;
            _emailClientService = emailClientService;
        }
        public Task Invoke()
        {
            var summary = _summaryRepository.GetSummary();
            var message = _emailMessagePreparator.PrepareDailySummaryMessage(summary);
            if(message == null)
            {
                return Task.CompletedTask;
            }
            _emailClientService.SendEmail(message);
            return Task.CompletedTask;
        }
    }
}
