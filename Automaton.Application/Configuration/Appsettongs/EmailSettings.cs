namespace Automaton.Application.Configuration.Appsettongs
{
    public class EmailSettings
    {
        public Imap Imap { get; set; }
        public Smtp Smtp { get; set; }
    }

    public class Imap
    {
        public string ImapServerName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
    }

    public class Smtp
    {
        public string SmtpServerName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public int Port { get; set; }
    }
}