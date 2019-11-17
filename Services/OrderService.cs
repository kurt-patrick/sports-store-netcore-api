using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SportsStoreApi.Entities;
using SportsStoreApi.Helpers;
using SportsStoreApi.Models;

namespace SportsStoreApi.Services
{
    public interface IOrderService
    {
        OrderSubmission Save(OrderSubmission order);
    }

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
