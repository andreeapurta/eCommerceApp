using eShop.Business.Models;
using eShop.Business.Services;
using eShop.UseCases.Interfaces.DataStore;
using eShop.UseCases.Interfaces.StateStore;
using eShop.UseCases.Interfaces.UI;
using System;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public class PlaceOrderUseCase : IPlaceOrderUseCase
    {
        private readonly IShoppingCart shoppingCart;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderService orderService;
        private readonly IShoppingCartStateStore shoppingCartStateStore;

        public PlaceOrderUseCase(IShoppingCart shoppingCart,
            IOrderRepository orderRepository,
            IOrderService orderService,
            IShoppingCartStateStore shoppingCartStateStore)
        {
            this.shoppingCart = shoppingCart;
            this.orderRepository = orderRepository;
            this.orderService = orderService;
            this.shoppingCartStateStore = shoppingCartStateStore;
        }

        public async Task<string> Execute(Order order)
        {
            await shoppingCart.UpdateOrderAsync(order);
            if (orderService.CheckCreateOrder(order))
            {
                order.DatePlaced = DateTime.Now;
                order.UniqueId = Guid.NewGuid().ToString();
                int orderId = orderRepository.CreateOrder(order);
                order = orderRepository.GetOrder(orderId);

                await shoppingCart.EmptyAsync();
                this.shoppingCartStateStore.LineItemsCountUpdated();

                return order.UniqueId;
            }

            return null;
        }
    }
}