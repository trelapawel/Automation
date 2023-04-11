using Automaton.Domain.Models;

namespace Automaton.Application.PaymentOrderMatching
{
    public interface IPaymentOrderMatcher
    {
        IEnumerable<MatchedPair> Match(IEnumerable<Order> orders, IEnumerable<Payment> payments);
    }
}
