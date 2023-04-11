namespace Automaton.Application.Email.EmailMessagePrepare.EmailTemplates
{
    public class DailySummaryTemplateModel
    {
        public int AllPaidOrders { get; set; }
        public int ThisMonthPaidOrders { get; set; }
        public int LastMonthPaidOrders { get; set; }
        public double Amount { get; set; }
    }
}
