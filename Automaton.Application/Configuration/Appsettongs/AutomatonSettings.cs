namespace Automaton.Application.Configuration.Appsettongs
{
    public class AutomatonSettings
    {
        public string AccountNumber { get; set; } = String.Empty;
        public double Price { get; set; }
        public string FilePassword { get; set; } = String.Empty;
        public string DailySummaryEmailAddress { get; set; } = String.Empty;
        public string CheckNewOrdersCron { get; set; } = String.Empty;
        public string CheckNewProductsCron { get; set; } = String.Empty;
        public string CheckNewPaymentsCron { get; set; } = String.Empty;
        public string DailySummaryCron { get; set; } = String.Empty;
    }
}
