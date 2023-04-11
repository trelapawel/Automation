using System.Text.RegularExpressions;

namespace Automaton.Application.ProductPreparation
{
    public class ProductUrlGetter : IProductUrlGetter
    {
        public string Get(string text)
        {
            var pattern = @">http.+</s";
            var meches = Regex.Matches(text, pattern);

            if (meches == null || meches.Count == 0)
            {
                throw new ArgumentException("Text contains no elements");
            }
            string url = meches[0].Value;
            url = url.Replace(">", string.Empty);
            url = url.Replace("</s", string.Empty);
            return url;
        }
    }
}
