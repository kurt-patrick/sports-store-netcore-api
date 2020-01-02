using System;
using System.Collections.Generic;
using SportsStoreApi.Entities;

namespace SportsStoreApi.Interfaces
{
    public interface IOrderService
    {
        Order SearchByOrderId(int userId, int orderId);
        IEnumerable<Order> SearchByDateRange(int userId, DateTime from, DateTime to);
        int Save(Order order);
        IEnumerable<Order> GetAll(int userId);
    }
}