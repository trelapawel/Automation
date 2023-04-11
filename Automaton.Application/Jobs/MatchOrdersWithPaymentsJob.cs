using Automaton.Application.Email;
using Automaton.Application.PaymentOrderMatching;
using Automaton.Domain.Repositories;
using Coravel.Invocable;
using Coravel.Queuing.Interfaces;

namespace Automaton.Application.Jobs
{
    public class MatchOrdersWithPaymentsJob : IInvocable
    {
        private readonly IPaymentReposotory _paymentReposotory;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentOrderMatcher _paymentOrderMatcher;
        private readonly IEmailClientService _emailClientService;
        private readonly IQueue _queue;

        public MatchOrdersWithPaymentsJob(
            IPaymentReposotory paymentReposotory,
            IOrderRepository orderRepository,
            IPaymentOrderMatcher paymentOrderMatcher,
            Email.IEmailClientService emailClientService,
            IQueue queue)
        {
            _paymentReposotory = paymentReposotory;
            _orderRepository = orderRepository;
            _paymentOrderMatcher = paymentOrderMatcher;
            _emailClientService = emailClientService;
            _queue = queue;
        }
        public Task Invoke()
        {
            var payments = _paymentReposotory.GetUnmachedPayments();
            if (!payments.Any())
            {
                return Task.CompletedTask;
            }
            var orders = _orderRepository.GetUnpaidOrders();
            if (!orders.Any())
            {
                return Task.CompletedTask;
            }
            var matches = _paymentOrderMatcher.Match(orders, payments);
            if (matches.Any())
            {
                foreach (var match in matches)
                {
                    var payment = payments.Single(x => x.Id == match.PaymentId);
                    var order = orders.Single(x => x.Id == match.OrderId);
                    _orderRepository.SetPaymentId(order, payment.Id);
                    _paymentReposotory.SetPaymentAsMatched(payment);
                    _emailClientService.SetMessageStatusToPaid(new List<uint> { Convert.ToUInt32(order.EmailMessageId) });
                }
                _queue.QueueInvocable<SendOrderedProductJob>();
            }
            return Task.CompletedTask;
        }
    }
}
