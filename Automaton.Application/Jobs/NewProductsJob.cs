using Automaton.Application.Email;
using Automaton.Application.ProductPreparation;
using Automaton.Domain.Repositories;
using Coravel.Invocable;

namespace Automaton.Application.Jobs
{
    public class NewProductsJob : IInvocable
    {
        private readonly IEmailClientService _emailClientService;
        private readonly IProductPreparator _productPreparator;
        private readonly IProductRepository _productRepository;

        public NewProductsJob(
            IEmailClientService emailClientService,
            IProductPreparator productPreparator,
            IProductRepository productRepository)
        {
            _emailClientService = emailClientService;
            _productPreparator = productPreparator;
            _productRepository = productRepository;
        }

        public Task Invoke()
        {
            try
            {
                var messages = _emailClientService.GetNewProductMessages();
                if (messages.Any())
                {
                    var products = _productPreparator.Prepare(messages);
                    _productRepository.AddProducts(products);
                    var result = _emailClientService.SetMessageStatusToProductAdded(messages.Select(x => x.Id).ToList());
                }
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}