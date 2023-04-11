using Automaton.Domain.Models;

namespace Automaton.Domain.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAddedOrders();
        IEnumerable<Order> GetUnpaidOrders();
        IEnumerable<Order> GetPaidOrders();
        Order GetLatestSameOrder(Order order);
        void AddOrders(IEnumerable<Order> orders);
        void UpdateMessageSentFlagToTrue(IEnumerable<Order> orders);
        void UpdateOrderSentFlagToTrue(IEnumerable<Order> orders);
        void SetPaymentId(Order order, int paymentId);
    }
}
