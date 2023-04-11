using Automaton.Application.Configuration.Appsettongs;
using Automaton.Application.Email;
using Automaton.Domain.Models;
using Microsoft.Extensions.Options;

namespace Automaton.Application.OrderPreparation
{
    public class OrderPreparator: IOrderPreparator
    {
        private readonly IOrderProductGetter _orderProductGetter;
        private readonly IOrderPreparatorEmailAddressGetter _orderPreparatorEmailAddressGetter;
        private readonly IOptions<AutomatonSettings> _settings;

        public OrderPreparator(
            IOrderProductGetter orderProductGetter,
            IOrderPreparatorEmailAddressGetter orderPreparatorEmailAddressGetter,
            IOptions<AutomatonSettings> settings)
        {
            _orderProductGetter = orderProductGetter;
            _orderPreparatorEmailAddressGetter = orderPreparatorEmailAddressGetter;
            _settings = settings;
        }

        public Order Prepare(EmailMessage message)
        {
            int productId = _orderProductGetter.GetProductId(message.Body);
            string customerEmailAddress = _orderPreparatorEmailAddressGetter.Get(message.Body);
            return new Order()
            {
                ProductId = productId,
                EmailMessageId = Convert.ToInt32(message.Id),
                CustomerEmailAddress = customerEmailAddress,
                CreatedDate = message.RecivedAt,
                Price = _settings.Value.Price
            };
        }
        public IEnumerable<Order> Prepare(IEnumerable<EmailMessage> messages)
        {
            List<Order> orders = new();
            foreach(EmailMessage message in messages)
            {
                orders.Add(Prepare(message));
            }
            return orders;
        }
    }
}
