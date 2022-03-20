using eShop.Business.Models;
using eShop.UseCases.Interfaces.DataStore;
using System.Collections.Generic;

namespace eShop.UseCases.SearchProductScreen
{
    public class SearchProductUseCase : ISearchProductUseCase
    {
        private readonly IProductRepository productRepository;

        public SearchProductUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IEnumerable<Product> Execute(string filter)
        {
            return productRepository.GetProducts(filter);
        }
    }
}