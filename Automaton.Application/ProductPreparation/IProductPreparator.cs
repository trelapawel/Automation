using Automaton.Application.Email;
using Automaton.Domain.Models;

namespace Automaton.Application.ProductPreparation
{
    public interface IProductPreparator
    {
        Product Prepare(EmailMessage emailMessage);
        IEnumerable<Product> Prepare(IEnumerable<EmailMessage> emailMessages);
    }
}
