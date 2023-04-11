using Automaton.Application.Email;
using Automaton.Domain.Models;

namespace Automaton.Application.OrderPreparation
{
    public interface IOrderPreparator
    {
        Order Prepare(EmailMessage message);
        IEnumerable<Order> Prepare(IEnumerable<EmailMessage> messages);
    }
}
