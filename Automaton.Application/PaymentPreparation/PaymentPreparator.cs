using Automaton.Application.Email;
using Automaton.Domain.Models;

namespace Automaton.Application.PaymentPreparation
{
    public class PaymentPreparator : IPaymentPreparator
    {
        private readonly IPaymentAmountGetter _paymentAmountGetter;
        private readonly IPaymentTittleGetter _paymentTittleGetter;

        public PaymentPreparator(
            IPaymentAmountGetter paymentAmountGetter,
            IPaymentTittleGetter paymentTittleGetter)
        {
            _paymentAmountGetter = paymentAmountGetter;
            _paymentTittleGetter = paymentTittleGetter;
        }
        public IEnumerable<Payment> Prepare(IEnumerable<EmailMessage> messages)
        {
            List<Payment> payments = new();
            foreach (EmailMessage message in messages)
            {
                payments.Add(Prepare(message));
            }
            return payments;
        }

        public Payment Prepare(EmailMessage message)
        {
            float amount = _paymentAmountGetter.Get(message.Body);
            string tittle = _paymentTittleGetter.Get(message.Body);

            return new Payment
            {
                Amount = amount,
                Tittle = tittle,
                CreatedDate = message.RecivedAt
            };
        }
    }
}
