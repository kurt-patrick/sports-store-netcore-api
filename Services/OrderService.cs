using System;
using System.Collections.Generic;
using SportsStoreApi.Interfaces;
using SportsStoreApi.Models;

namespace SportsStoreApi.Services
{
    public class OrderService : IOrderService
    {
        private static List<OrderSubmission> _orders = new List<OrderSubmission>();
        public OrderSubmission Save(OrderSubmission order)
        {
            Console.WriteLine("OrderService.Save()");
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            _orders.Add(order);
            Console.WriteLine("Orders count: " + _orders.Count.ToString());
            return order;
        }
    }

}
