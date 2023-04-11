using System.Text.RegularExpressions;

namespace Automaton.Application.ProductPreparation
{
    public class ProductIdGetter : IProductIdGetter
    {
        public int Get(string text)
        {
            var pattern = @">\d+<";
            var meches = Regex.Matches(text, pattern);

            if (meches == null || meches.Count > 1)
            {
                throw new ArgumentException("Text contains more than one maching element");
            }
            string stringId = meches.Single().Value;
            stringId = stringId.Replace(">", string.Empty);
            stringId = stringId.Replace("<", string.Empty);
            return int.Parse(stringId);
        }
    }
}
