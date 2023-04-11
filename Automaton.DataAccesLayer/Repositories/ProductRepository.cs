using Automaton.DataAccesLayer.Context;
using Automaton.Domain.Models;
using Automaton.Domain.Repositories;

namespace Automaton.DataAccesLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IAutomatonDbContext _automatonDbContext;

        public ProductRepository(
            IAutomatonDbContext automatonDbContext)
        {
            _automatonDbContext = automatonDbContext;
        }
        public void AddProducts(IEnumerable<Product> products)
        {
            _automatonDbContext.Products.AddRange(products);
            _automatonDbContext.Save();
        }

        public Product GetById(int productId)
        {
            return _automatonDbContext.Products.Where(x => x.Id == productId).FirstOrDefault();
        }
    }
}
