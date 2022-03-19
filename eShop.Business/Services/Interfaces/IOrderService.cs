using eShop.Business.Models;

namespace eShop.Business.Services
{
    public interface IOrderService
    {
        bool CheckCreateOrder(Order order);

        bool CheckCustomerInformation(string name, string address, string city, string province, string country);

        bool CheckeProcessOrder(Order order);

        bool CheckUpdateOrder(Order order);
    }
}