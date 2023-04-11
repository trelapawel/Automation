using System.Text.RegularExpressions;

namespace Automaton.Application.OrderPreparation
{
    public class OrderProductGetter : IOrderProductGetter
    {
        public int GetProductId(string text)
        {
            var pattern = @"\d+\.\s+[a-zA-Z \d]+";
            var meches = Regex.Matches(text, pattern);

            if(meches == null || meches.Count == 0)
            {
                throw new ArgumentException("Text contains more than one maching element");
            }
            string product = meches.Single().Value;
            string stringProductId = product.Substring(0, product.IndexOf("."));
            if(!int.TryParse(stringProductId, out int productId))
            {
                throw new Exception("Cant extract product id out of string");
            }
            return productId;
        }
    }
}
