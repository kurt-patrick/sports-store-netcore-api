using SportsStoreApi.Entities;

namespace SportsStoreApi.Interfaces
{
    public interface IOrderService
    {
        int Save(Order order);
    }
}