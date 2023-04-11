namespace Automaton.Domain.Models
{
    public class Summary
    {
        public int AllPaidOrders { get; set; }
        public int ThisMonthPaidOrders { get; set; }
        public int LastMonthPaidOrders { get; set; }
        public double Amount { get; set; }
    }
}
