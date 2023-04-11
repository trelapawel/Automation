using Automaton.Domain.Models;

namespace Automaton.Domain.Repositories
{
    public interface IProductRepository
    {
        void AddProducts(IEnumerable<Product> products);
        Product GetById(int productId);
    }
}
