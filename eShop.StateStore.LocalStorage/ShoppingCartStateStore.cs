using eShop.UseCases.Interfaces.StateStore;
using eShop.UseCases.Interfaces.UI;
using System.Threading.Tasks;

namespace eShop.StateStore.LocalStorage
{
    public class ShoppingCartStateStore : StateStoreBase, IShoppingCartStateStore
    {
        private readonly IShoppingCart shoppingCart;

        public ShoppingCartStateStore(IShoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
        }

        public async Task<int> GetLineItemsCount()
        {
            var order = await shoppingCart.GetOrderAsync();
            if (order != null && order.LineItems != null && order.LineItems.Count > 0)
            {
                return order.LineItems.Count;
            }
            return 0;
        }

        public void LineItemsCountUpdated()
        {
            base.BroadCastStateChange();
        }

        public void ProductQuantityUpdated()
        {
            base.BroadCastStateChange();
        }
    }
}