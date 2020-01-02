using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsStoreApi.Entities;
using SportsStoreApi.Interfaces;

namespace SportsStoreApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly StoreContext _storeContext;

        public OrderService(StoreContext storeContext)
        {
            if(storeContext == null)
            {
                throw new ArgumentNullException(nameof(storeContext));
            }
            this._storeContext = storeContext;
            // this call is required for the table to be created
            storeContext.Database.EnsureCreated();
        }

        public Order SearchByOrderId(int userId, int id)
        {
            Console.WriteLine($"OrderService.SearchByOrderId({userId}, {id}");
            return _storeContext.Orders
                .Include(order => order.Items)
                .FirstOrDefault(order => order.Id == id && order.UserId == userId);
        }


        public int Save(Order order)
        {
            Console.WriteLine("OrderService.Save()");
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            _storeContext.Orders.Add(order);
            int id = _storeContext.SaveChanges();
            Console.WriteLine($"Order.Save() id: {id}");
            return id;
        }

        public IEnumerable<Order> GetAll(int userId)
        {
            Console.WriteLine($"OrderService.GetAll({userId})");
            return _storeContext.Orders
                .Where(order => order.UserId == userId)
                .ToList();
        }

        public IEnumerable<Order> SearchByDateRange(int userId, DateTime from, DateTime to)
        {
            to = to.AddDays(1);
            Console.WriteLine($"OrderService.SearchByDateRange({from}, {to})");
            return _storeContext.Orders
                .Where(order => order.OrderDate >= from && order.OrderDate <= to)
                .Where(order => order.UserId == userId)
                .ToList();
        }

    } // class OrderService

} 
