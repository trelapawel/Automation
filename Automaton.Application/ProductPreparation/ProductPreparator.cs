using Automaton.Application.Email;
using Automaton.Domain.Models;

namespace Automaton.Application.ProductPreparation
{
    public class ProductPreparator : IProductPreparator
    {
        private readonly IProductIdGetter _productIdGetter;
        private readonly IProductNameGetter _productNameGetter;
        private readonly IProductUrlGetter _productUrlGetter;

        public ProductPreparator(
            IProductIdGetter productIdGetter,
            IProductNameGetter productNameGetter,
            IProductUrlGetter productUrlGetter)
        {
            _productIdGetter = productIdGetter;
            _productNameGetter = productNameGetter;
            _productUrlGetter = productUrlGetter;
        }
        public Product Prepare(EmailMessage emailMessage)
        {
            int productId = _productIdGetter.Get(emailMessage.Body);
            string name = _productNameGetter.Get(emailMessage.Body);
            string url = _productUrlGetter.Get(emailMessage.Body);

            return new Product
            {
                Id = productId,
                Name = name,
                UrlToFile = url
            };
        }

        public IEnumerable<Product> Prepare(IEnumerable<EmailMessage> emailMessages)
        {
            List<Product> products = new();
            foreach (EmailMessage message in emailMessages)
            {
                products.Add(Prepare(message));
            }
            return products;
        }
    }
}
