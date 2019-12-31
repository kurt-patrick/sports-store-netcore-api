using System.Collections.Generic;
using SportsStoreApi.Entities;

namespace SportsStoreApi.Interfaces
{
    public interface IOrderService
    {
        Order GetByOrderId(int orderId);
        int Save(Order order);
        IEnumerable<Order> GetAll();
    }
}