using Automaton.Domain.Models;

namespace Automaton.Domain.Repositories
{
    public interface IPaymentReposotory
    {
        void AddPayments(IEnumerable<Payment> payments);
        IEnumerable<Payment> GetUnmachedPayments();
        void SetPaymentAsMatched(Payment payment);
    }
}
