using System;
using System.Collections.Generic;
using SportsStoreApi.Entities;

namespace SportsStoreApi.Interfaces
{
    public interface IOrderService
    {
        Order SearchByOrderId(int orderId);
        IEnumerable<Order> SearchByDateRange(DateTime from, DateTime to);
        int Save(Order order);
        IEnumerable<Order> GetAll();
    }
}