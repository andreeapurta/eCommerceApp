using eShop.Business.Models;
using eShop.UseCases.Interfaces.DataStore;

namespace eShop.UseCases.ViewProductScreen
{
    public class ViewProductUseCase : IViewProductUseCase
    {
        private readonly IProductRepository productRepository;

        public ViewProductUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public Product Execute(int id)
        {
            return productRepository.GetProduct(id);
        }
    }
}