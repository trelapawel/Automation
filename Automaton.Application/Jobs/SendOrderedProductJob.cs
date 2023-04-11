using Automaton.Application.Email;
using Automaton.Application.Email.EmailMessagePrepare;
using Automaton.Domain.Repositories;
using Coravel.Invocable;

namespace Automaton.Application.Jobs
{
    public class SendOrderedProductJob : IInvocable
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailClientService _emailClientService;
        private readonly IEmailMessagePreparator _emailMessagePreparator;

        public SendOrderedProductJob(
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
            var paidOrders = _orderRepository.GetPaidOrders();
            if (paidOrders.Any())
            {
                var messages = _emailMessagePreparator.PrepareMessagesWithOrderedProduct(paidOrders);
                _emailClientService.SendEmails(messages);
                _orderRepository.UpdateOrderSentFlagToTrue(paidOrders);
                _emailClientService.SetMessageStatusOrderSent(paidOrders.Select(x => Convert.ToUInt32(x.EmailMessageId)).ToList());
            }
            return Task.CompletedTask;
        }
    }
}
