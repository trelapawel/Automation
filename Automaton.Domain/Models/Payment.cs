namespace Automaton.Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Tittle { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public bool IsMached { get; set; }

        public void SetPaymentAsMatched()
        {
            IsMached = true;
        }
    }
}
