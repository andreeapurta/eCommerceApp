using eShop.Business.Models;
using eShop.UseCases.Interfaces.DataStore;

namespace eShop.UseCases.ViewProductScreen
{
    public class ViewProductUseCase : IViewProductUseCase
    {
        private IProductRepository ProductRepository { get; set; }

        public ViewProductUseCase(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public Product Execute(int id)
        {
            return ProductRepository.GetProduct(id);
        }
    }
}