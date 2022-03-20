using System.Threading.Tasks;

namespace eShop.UseCases.Interfaces.StateStore
{
    public interface IShoppingCartStateStore : IStateStore
    {
        Task<int> GetLineItemsCount();

        void LineItemsCountUpdated();

        void ProductQuantityUpdated();
    }
}