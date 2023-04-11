using Automaton.Application.Email;
using Automaton.Application.OrderPreparation;
using Automaton.Domain.Models;
using Automaton.Domain.Repositories;
using Coravel.Invocable;
using Coravel.Queuing.Interfaces;

namespace Automaton.Application.Jobs
{
    public class NewOrdersJob : IInvocable
    {
        private readonly IEmailClientService _emailClientService;
        private readonly IOrderPreparator _orderPreparator;
        private readonly IOrderRepository _orderRepository;
        private readonly INewOrdersFinder _newOrdersFinder;
        private readonly IQueue _queue;

        public NewOrdersJob(
            IEmailClientService emailClientService,
            IOrderPreparator orderPreparator,
            IOrderRepository orderRepository,
            INewOrdersFinder newOrdersFinder,
            IQueue queue)
        {
            _emailClientService = emailClientService;
            _orderPreparator = orderPreparator;
            _orderRepository = orderRepository;
            _newOrdersFinder = newOrdersFinder;
            _queue = queue;
        }
        public Task Invoke()
        {
            try
            {
                var messages = _emailClientService.GetNewOrderMessages();
                if (messages.Any())
                {
                    var orders = _orderPreparator.Prepare(messages);
                    var newOrders = _newOrdersFinder.FindNewOrders(orders);
                    var duplicates = orders.Except(newOrders);

                    MarkDuplicates(duplicates);
                    AddNewOrders(newOrders);
                    _queue.QueueInvocable<SendOrderSummaryJob>();
                }
                return Task.CompletedTask;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void MarkDuplicates(IEnumerable<Order> orders)
        {
            var result = _emailClientService.SetMessageStatusToDuplicate(orders.Select(x => (uint)x.EmailMessageId).ToList());
        }

        public void AddNewOrders(IEnumerable<Order> orders)
        {
            _orderRepository.AddOrders(orders);
            var result = _emailClientService.SetMessageStatusToOrderAdded(orders.Select(x => (uint)x.EmailMessageId).ToList());
        }
    }
}
