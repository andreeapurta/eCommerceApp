using eShop.Business.Models;
using System.Collections.Generic;

namespace eShop.UseCases.Interfaces.DataStore
{
    public interface IOrderRepository
    {
        Order GetOrder(int id);

        Order GetOrderByUniqueId(string uniqueId);

        int CreateOrder(Order order);

        void UpdateOrder(Order order);

        IEnumerable<Order> GetOrders();

        IEnumerable<Order> GetOutstandingOrders();

        IEnumerable<Order> GetProcessedOrders();

        IEnumerable<OrderLineItem> GetLineItemsByOrderId(int orderId);
    }
}