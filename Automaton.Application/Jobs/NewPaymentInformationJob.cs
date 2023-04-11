using Automaton.Application.Email;
using Automaton.Application.PaymentPreparation;
using Automaton.Domain.Repositories;
using Coravel.Invocable;
using Coravel.Queuing.Interfaces;

namespace Automaton.Application.Jobs
{
    public class NewPaymentInformationJob : IInvocable
    {
        private readonly IEmailClientService _emailClientService;
        private readonly IPaymentReposotory _paymentReposotory;
        private readonly IPaymentPreparator _paymentPreparator;
        private readonly IQueue _queue;

        public NewPaymentInformationJob(
            IEmailClientService emailClientService,
            IPaymentReposotory paymentReposotory,
            IPaymentPreparator paymentPreparator,
            IQueue queue)
        {
            _emailClientService = emailClientService;
            _paymentReposotory = paymentReposotory;
            _paymentPreparator = paymentPreparator;
            _queue = queue;
        }
        public Task Invoke()
        {
            try
            {
                var messages = _emailClientService.GetNewPaymentMessages();
                if (messages.Any())
                {
                    var payments = _paymentPreparator.Prepare(messages);
                    _paymentReposotory.AddPayments(payments);
                    var result = _emailClientService.SetMessageStatusToPaymentAdded(messages.Select(x => x.Id).ToList());
                    _queue.QueueInvocable<MatchOrdersWithPaymentsJob>();
                }
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
