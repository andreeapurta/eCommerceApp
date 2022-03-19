using eShop.Business.Models;
using eShop.UseCases.Interfaces.DataStore;
using System.Collections.Generic;

namespace eShop.UseCases.SearchProductScreen
{
    public class SearchProductUseCase : ISearchProductUseCase
    {
        private IProductRepository ProductRepository { get; set; }

        public SearchProductUseCase(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public IEnumerable<Product> Execute(string filter)
        {
            return ProductRepository.GetProducts(filter);
        }
    }
}