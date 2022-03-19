﻿using eShop.Business.Models;

namespace eShop.Business.Services
{
    public class OrderService : IOrderService
    {
        public bool CheckCustomerInformation(string name, string address, string city, string province, string country)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(province) ||
                string.IsNullOrWhiteSpace(country)) return false;

            return true;
        }

        public bool CheckCreateOrder(Order order)
        {
            //order has to existi
            if (order == null) return false;

            //order has to have order line items
            if (order.LineItems == null || order.LineItems.Count <= 0) return false;

            //validating line items
            foreach (var item in order.LineItems)
            {
                if (item.ProductId <= 0 || item.Price < 0 || item.Quantity <= 0) return false;
            }

            //validate customer info
            if (!CheckCustomerInformation(order.CustomerName, order.CustomerAddress, order.CustomerCity, order.CustomerStateProvince, order.CustomerCountry))
                return false;

            return true;
        }

        public bool CheckUpdateOrder(Order order)
        {
            //order has to existi
            if (order == null) return false;
            if (!order.OrderId.HasValue) return false;

            //order has to have order line items
            if (order.LineItems == null || order.LineItems.Count <= 0) return false;

            //Placed Date has to be populated
            if (!order.DatePlaced.HasValue) return false;

            //Other dates
            if (order.DateProcessed.HasValue || order.DateProcessing.HasValue) return false;

            //valiate uniqueId
            if (string.IsNullOrWhiteSpace(order.UniqueId)) return false;

            //validating line items
            foreach (var item in order.LineItems)
            {
                if (item.ProductId <= 0 || item.Price < 0 || item.Quantity <= 0 || item.OrderId == order.OrderId)
                    return false;
            }

            //validate customer info
            if (!CheckCustomerInformation(order.CustomerName,
                    order.CustomerAddress,
                    order.CustomerCity,
                    order.CustomerStateProvince,
                    order.CustomerCountry)) return false;

            return true;
        }

        public bool CheckeProcessOrder(Order order)
        {
            if (!order.DateProcessed.HasValue || string.IsNullOrWhiteSpace(order.AdminUser))
                return false;

            return true;
        }
    }
}