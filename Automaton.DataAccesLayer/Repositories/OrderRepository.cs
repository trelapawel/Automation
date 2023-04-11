using Automaton.DataAccesLayer.Context;
using Automaton.Domain.Models;
using Automaton.Domain.Repositories;

namespace Automaton.DataAccesLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IAutomatonDbContext _automatonDbContext;

        public OrderRepository(
            IAutomatonDbContext automatonDbContext)
        {
            _automatonDbContext = automatonDbContext;
        }

        public void AddOrders(IEnumerable<Order> orders)
        {
            _automatonDbContext.Orders.AddRange(orders);
            _automatonDbContext.Save();
        }

        public IEnumerable<Order> GetAddedOrders()
        {
            var addedOrders = _automatonDbContext.Orders.Where(x => x.MessageSent == false);
            return addedOrders;
        }

        public Order GetLatestSameOrder(Order order)
        {
            return _automatonDbContext.Orders
                .Where(x => x.CustomerEmailAddress == order.CustomerEmailAddress && x.ProductId == order.ProductId)
                .OrderByDescending(x => x.CreatedDate).FirstOrDefault();
        }

        public IEnumerable<Order> GetPaidOrders()
        {
            return _automatonDbContext.Orders.Where(x => !x.OrderSent && x.PaymentId != null).ToList();
        }

        public IEnumerable<Order> GetUnpaidOrders()
        {
            return _automatonDbContext.Orders.Where(x => x.MessageSent).ToList();
        }

        public void SetPaymentId(Order order, int paymentId)
        {
            order.SetPaymentId(paymentId);
            _automatonDbContext.Save();
        }

        public void UpdateMessageSentFlagToTrue(IEnumerable<Order> orders)
        {
            foreach(var order in orders)
            {
                order.SetMessageSentFlagToTrue();
            }
            _automatonDbContext.Save();
        }

        public void UpdateOrderSentFlagToTrue(IEnumerable<Order> orders)
        {
            foreach(var order in orders)
            {
                order.UpdateOrderSentFlagToTrue();
            }
            _automatonDbContext.Save();
        }
    }
}
