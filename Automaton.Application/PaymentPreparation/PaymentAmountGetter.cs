using System.Globalization;
using System.Text.RegularExpressions;

namespace Automaton.Application.PaymentPreparation
{
    public class PaymentAmountGetter : IPaymentAmountGetter
    {
        public float Get(string text)
        {
            var pattern = @"\d+,\w+ PLN";
            var meches = Regex.Matches(text, pattern);

            if (meches == null)
            {
                throw new ArgumentException("Text contains no maching element");
            }
            string amount = meches.Single().Value;
            amount = amount.Replace(" PLN", "");
            amount = amount.Replace(",", ".");
            return float.Parse(amount, CultureInfo.InvariantCulture);
        }
    }
}
