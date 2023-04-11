using Automaton.Application.Email;
using Automaton.Domain.Models;

namespace Automaton.Application.PaymentPreparation
{
    public interface IPaymentPreparator
    {
        IEnumerable<Payment> Prepare(IEnumerable<EmailMessage> messages);
    }
}
