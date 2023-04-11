using Automaton.DataAccesLayer.Context;
using Automaton.Domain.Models;
using Automaton.Domain.Repositories;

namespace Automaton.DataAccesLayer.Repositories
{
    public  class PaymentReposotory : IPaymentReposotory
    {
        private readonly IAutomatonDbContext _automatonDbContext;

        public PaymentReposotory(
            IAutomatonDbContext automatonDbContext)
        {
            _automatonDbContext = automatonDbContext;
        }

        public void AddPayments(IEnumerable<Payment> payments)
        {
            _automatonDbContext.Payments.AddRange(payments);
            _automatonDbContext.Save();
        }

        public IEnumerable<Payment> GetUnmachedPayments()
        {
            var payments = _automatonDbContext.Payments.Where(x => !x.IsMached).ToList();
            return payments;
        }
        public void SetPaymentAsMatched(Payment payment)
        {
            payment.SetPaymentAsMatched();
            _automatonDbContext.Save();
        }
    }
}
