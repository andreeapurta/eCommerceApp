using eShop.Business.Models;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public interface IViewShoppingCartUseCase
    {
        Task<Order> Execute();
    }
}