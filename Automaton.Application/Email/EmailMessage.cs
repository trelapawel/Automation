namespace Automaton.Application.Email
{
    public class EmailMessage
    {
        public uint Id { get; set; }
        public string From { get; set; } = String.Empty;
        public string To { get; set; } = String.Empty;
        public string Subject { get; set; } = String.Empty;
        public string Body { get; set; } = String.Empty;
        public DateTime RecivedAt { get; set; }
    }
}
