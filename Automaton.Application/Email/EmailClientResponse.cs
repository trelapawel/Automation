namespace Automaton.Application.Email
{
    public class EmailClientResponse
    {
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; } = Array.Empty<string>();

        private EmailClientResponse()
        {

        }

        public static EmailClientResponse Success()
        {
            return new EmailClientResponse
            {
                IsSuccess = true,
                Errors = Array.Empty<string>()
            };
        }

        public static EmailClientResponse Failure(string[] errors)
        {
            return new EmailClientResponse
            {
                IsSuccess = false,
                Errors = errors
            };
        }
        public static EmailClientResponse Failure(string error)
        {
            return new EmailClientResponse
            {
                IsSuccess = false,
                Errors = new string[] { error }
            };
        }
    }
}
