using eShop.Business.Models;
using System.Threading.Tasks;

namespace eShop.UseCases.Interfaces.UI
{
    public interface IShoppingCart
    {
        Task<Order> GetOrderAsync();

        Task<Order> AddProductAsync(Product product);

        Task<Order> UpdateQuantityAsync(int productId, int quantity);

        Task UpdateOrderAsync(Order order);

        Task<Order> DeleteProductAsync(int productId);

        Task<Order> PlaceOrderAsync();

        Task EmptyAsync();
    }
}