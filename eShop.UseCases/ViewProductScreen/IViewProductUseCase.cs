using eShop.Business.Models;

namespace eShop.UseCases.ViewProductScreen
{
    public interface IViewProductUseCase
    {
        Product Execute(int id);
    }
}