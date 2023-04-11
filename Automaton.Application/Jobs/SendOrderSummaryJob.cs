using Automaton.Application.Email;
using Automaton.Application.Email.EmailMessagePrepare;
using Automaton.Domain.Repositories;
using Coravel.Invocable;

namespace Automaton.Application.Jobs
{
    public class SendOrderSummaryJob : IInvocable
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailClientService _emailClientService;
        private readonly IEmailMessagePreparator _emailMessagePreparator;

        public SendOrderSummaryJob(
            IOrderRepository orderRepository,
            IEmailClientService emailClientService,
            IEmailMessagePreparator emailMessagePreparator)
        {
            _orderRepository = orderRepository;
            _emailClientService = emailClientService;
            _emailMessagePreparator = emailMessagePreparator;
        }

        public Task Invoke()
        {
            var orders = _orderRepository.GetAddedOrders();
            if (orders.Any())
            {
                var messageIdsToUpdate = orders.Select(x => Convert.ToUInt32(x.EmailMessageId)).ToList();
                var emailMessages = _emailMessagePreparator.PrepareOrderSummaryMessages(orders);
                _orderRepository.UpdateMessageSentFlagToTrue(orders);
                _emailClientService.SendEmails(emailMessages);
                _emailClientService.SetMessageStatusToNotPaid(messageIdsToUpdate);
            }
            return Task.CompletedTask;
        }
    }
}
