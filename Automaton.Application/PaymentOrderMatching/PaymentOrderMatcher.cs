using Automaton.Domain.Models;

namespace Automaton.Application.PaymentOrderMatching
{
    public class PaymentOrderMatcher : IPaymentOrderMatcher
    {
        public IEnumerable<MatchedPair> Match(IEnumerable<Order> orders, IEnumerable<Payment> payments)
        {
            List<MatchedPair> matches = new();
            foreach(var payment in payments)
            {
                var matchedOrder = Match(payment, orders);
                if(matchedOrder != null)
                {
                    matches.Add(new MatchedPair
                    {
                        PaymentId = payment.Id,
                        OrderId = matchedOrder.Id
                    });
                }
            }
            return matches;
        }

        public Order Match(Payment payment, IEnumerable<Order> orders)
        {
            foreach(var order in orders)
            {
                if(Match(payment, order))
                {
                    return order;
                }
            }
            return null;
        }

        public bool Match(Payment payment, Order order)
        {
            return payment.Amount >= order.Price 
                && payment.Tittle.Contains(order.Id.ToString());
        }
    }
}
