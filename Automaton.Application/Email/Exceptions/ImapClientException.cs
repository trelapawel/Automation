namespace Automaton.Application.Email.Exceptions
{
    public class ImapClientException : Exception
    {
        public ImapClientException(string message) : base(message) 
        { 

        }

        public ImapClientException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
