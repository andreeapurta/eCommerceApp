using eShop.Business.Models;
using eShop.UseCases.Interfaces.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eShop.Data.InMemory
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Dictionary<int, Order> orders;

        public OrderRepository()
        {
            orders = new Dictionary<int, Order>();
        }

        public int CreateOrder(Order order)
        {
            order.OrderId = orders.Count + 1;
            order.UniqueId = Guid.NewGuid().ToString();
            orders.Add(order.OrderId.Value, order);
            return order.OrderId.Value;
        }

        public IEnumerable<Order> GetOrders()
        {
            return orders.Values;
        }

        public IEnumerable<Order> GetOutstandingOrders()
        {
            var allOrders = (IEnumerable<Order>)orders.Values;
            return allOrders.Where(x => x.DateProcessed.HasValue == false);
        }

        public IEnumerable<Order> GetProcessedOrders()
        {
            var allOrders = (IEnumerable<Order>)orders.Values;
            return allOrders.Where(x => x.DateProcessed.HasValue);
        }

        public Order GetOrder(int id)
        {
            return orders[id];
        }

        public Order GetOrderByUniqueId(string uniqueId)
        {
            foreach (var order in orders)
                if (order.Value.UniqueId == uniqueId) return order.Value;

            return null;
        }

        public void UpdateOrder(Order order)
        {
            if (order == null || !order.OrderId.HasValue) return;

            var ord = orders[order.OrderId.Value];
            if (ord == null) return;

            orders[order.OrderId.Value] = order;
        }

        public IEnumerable<OrderLineItem> GetLineItemsByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}