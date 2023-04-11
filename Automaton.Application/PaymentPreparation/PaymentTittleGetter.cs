using System.Text.RegularExpressions;

namespace Automaton.Application.PaymentPreparation
{
    public class PaymentTittleGetter : IPaymentTittleGetter
    {
        public string Get(string text)
        {
            //todo
            var pattern = @"tytułem.+</p>";
            var meches = Regex.Matches(text, pattern);

            if (meches == null)
            {
                throw new ArgumentException("Text contains no maching element");
            }
            var tittle = meches.Single().Value;
            tittle = tittle.Replace("tytułem ", "");
            tittle = tittle.Replace(".</p>", "");
            return tittle;
        }
    }
}

