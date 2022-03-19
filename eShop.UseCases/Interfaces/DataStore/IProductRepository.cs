using eShop.Business.Models;
using System.Collections.Generic;

namespace eShop.UseCases.Interfaces.DataStore
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(string filter = null);

        Product GetProduct(int id);
    }
}