using eShop.Business.Models;
using eShop.UseCases.Interfaces.DataStore;
using eShop.UseCases.Interfaces.UI;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.ShoppingCart.LocalStorage
{
    public class ShoppingCart : IShoppingCart
    {
        private const string shoppingCartLocalStorageKey = "eShop.ShoppingCart";
        private readonly IJSRuntime jsRuntime;
        private readonly IProductRepository productRepository;

        public ShoppingCart(IJSRuntime jsRuntime, IProductRepository productRepository)
        {
            this.jsRuntime = jsRuntime;
            this.productRepository = productRepository;
        }

        public async Task<Order> AddProductAsync(Product product)
        {
            var order = await GetOrderFromLocalStorage();
            order.AddProduct(product.Id, 1, product.Price);
            await SetOrderInLocalStorage(order);
            return order;
        }

        public async Task<Order> DeleteProductAsync(int productId)
        {
            var order = await GetOrderFromLocalStorage();
            order.RemoveProduct(productId);
            await SetOrderInLocalStorage(order);

            return order;
        }

        public Task EmptyAsync()
        {
            return this.SetOrderInLocalStorage(null);
        }

        public async Task<Order> GetOrderAsync()
        {
            var order = await GetOrderFromLocalStorage();
            return order;
        }

        public Task<Order> PlaceOrderAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateOrderAsync(Order order)
        {
            return this.SetOrderInLocalStorage(order);
        }

        public async Task<Order> UpdateQuantityAsync(int productId, int quantity)
        {
            var order = await GetOrderFromLocalStorage();

            if (quantity < 0)
            {
                return order;
            }
            else if (quantity == 0)
            {
                return await DeleteProductAsync(productId);
            }

            var item = order.LineItems.SingleOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
            }

            await SetOrderInLocalStorage(order);
            return order;
        }

        private async Task SetOrderInLocalStorage(Order order)
        {
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", shoppingCartLocalStorageKey, JsonConvert.SerializeObject(order));
        }

        private async Task<Order> GetOrderFromLocalStorage()
        {
            Order order = null;

            var orderKey = await jsRuntime.InvokeAsync<string>("localStorage.getItem", shoppingCartLocalStorageKey);
            //aici de testat daca pun " " in EmptyAsync, poate nu mai trebe conditia 2
            if (!string.IsNullOrWhiteSpace(orderKey) && orderKey.ToLower() != "null")
            {
                order = JsonConvert.DeserializeObject<Order>(orderKey);
            }
            else
            {
                order = new Order();
                await SetOrderInLocalStorage(order);
            }

            //getting the products?!
            foreach (var item in order.LineItems)
            {
                item.Product = productRepository.GetProduct(item.ProductId);
            }

            return order;
        }
    }
}