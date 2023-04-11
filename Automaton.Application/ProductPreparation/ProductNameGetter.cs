using System.Text.RegularExpressions;

namespace Automaton.Application.ProductPreparation
{
    public class ProductNameGetter : IProductNameGetter
    {
        public string Get(string text)
        {
            var pattern = @"(?=>)[^<]*(?=<\/span)";
            var meches = Regex.Matches(text, pattern);

            if (meches == null || meches.Count == 0)
            {
                throw new ArgumentException("Text contains no elements");
            }
            string name = meches[1].Value;
            name = name.Replace(">", string.Empty);
            return name;
        }
    }
}
