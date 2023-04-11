using Automaton.Domain.Models;

namespace Automaton.Application.OrderPreparation
{
    public interface INewOrdersFinder
    {
        IEnumerable<Order> FindNewOrders(IEnumerable<Order> orders);
    }
}
