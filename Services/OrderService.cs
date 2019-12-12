using System;
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
    }

}
