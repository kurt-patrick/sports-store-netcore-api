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
        Order Save(OrderSubmission order);
    }

    public class OrderService : IOrderService
    {
        private List<Order> _orders = new List<Order>();
        public Order Save(OrderSubmission order)
        {
            Order newOrder = order as Order;
            _orders.Add(newOrder);
            return newOrder;
        }
    }

}
