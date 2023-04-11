using Automaton.Domain.Models;
using Automaton.Domain.Repositories;

namespace Automaton.Application.OrderPreparation
{
    public class NewOrdersFinder : INewOrdersFinder
    {
        private readonly IOrderRepository _orderRepository;

        public NewOrdersFinder(
            IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public IEnumerable<Order> FindNewOrders(IEnumerable<Order> orders)
        {
            var list = new List<Order>();
            foreach(var order in orders.OrderBy(x => x.CreatedDate))
            {
                var sameOrder = list
                    .Where(x => x.ProductId == order.ProductId 
                                && x.CustomerEmailAddress == order.CustomerEmailAddress)
                    .FirstOrDefault();
                if(sameOrder == null)
                {
                    sameOrder = _orderRepository.GetLatestSameOrder(order);
                    if (sameOrder == null)
                    {
                        list.Add(order);
                        continue;
                    }
                    if(IsDateOlderThanOneDay(sameOrder.CreatedDate, order.CreatedDate))
                    {
                        list.Add(order);
                        continue;
                    }
                    continue;
                }
                if(IsDateOlderThanOneDay(sameOrder.CreatedDate, order.CreatedDate))
                {
                    list.Add(order);
                }
            }
            return list;
        }

        public bool IsDateOlderThanOneDay(DateTime oldDate, DateTime newDate)
        {
            return oldDate.AddDays(1) < newDate;
        }
    }
}
