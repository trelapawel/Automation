using System.Text.RegularExpressions;

namespace Automaton.Application.OrderPreparation
{
    public class OrderPreparatorEmailAddressGetter : IOrderPreparatorEmailAddressGetter
    {
        public string Get(string text)
        {
            var pattern = @"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+";
            var meches = Regex.Matches(text, pattern);

            if (meches == null || meches.Count == 0)
            {
                throw new ArgumentException("Text contains more than one maching element");
            }
            string email = meches.Single().Value;
            return email;
        }
    }
}
