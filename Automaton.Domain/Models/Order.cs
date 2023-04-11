namespace Automaton.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int EmailMessageId { get; set; }
        public string CustomerEmailAddress { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public double Price { get; set; }
        public int? PaymentId { get; set; }
        public bool MessageSent { get; set; }
        public bool OrderSent { get; set; }

        public void SetMessageSentFlagToTrue()
        {
            MessageSent = true;
        }

        public void SetPaymentId(int paymentId)
        {
            PaymentId = paymentId;
        }

        public void UpdateOrderSentFlagToTrue()
        {
            OrderSent = true;
        }
    }
}