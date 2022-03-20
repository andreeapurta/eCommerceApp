using eShop.Business.Models;
using eShop.UseCases.Interfaces.StateStore;
using eShop.UseCases.Interfaces.UI;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public class UpdateQuantityUseCase : IUpdateQuantityUseCase
    {
        private readonly IShoppingCart shoppingCart;
        private readonly IShoppingCartStateStore shoppingCartStateStore;

        public UpdateQuantityUseCase(IShoppingCart shoppingCart, IShoppingCartStateStore shoppingCartStateStore)
        {
            this.shoppingCart = shoppingCart;
            this.shoppingCartStateStore = shoppingCartStateStore;
        }

        public async Task<Order> Execute(int productId, int quanity)
        {
            var order = await shoppingCart.UpdateQuantityAsync(productId, quanity);
            shoppingCartStateStore.ProductQuantityUpdated();
            return order;
        }
    }
}