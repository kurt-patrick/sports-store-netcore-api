using SportsStoreApi.Models;

namespace SportsStoreApi.Interfaces
{
    public interface IOrderService
    {
        OrderSubmission Save(OrderSubmission order);
    }
}