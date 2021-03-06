﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportsStoreApi.Services;
using SportsStoreApi.Models;
using System.Linq;
using System;
using SportsStoreApi.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using System.Collections.Generic;
using SportsStoreApi.Entities;
using SportsStoreApi.Helpers;

namespace SportsStoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public OrdersController(IUserService userService, IProductService productService, IOrderService orderService, StoreContext storeContext, IMemoryCache cache)
        {
            if(storeContext == null)
            {
                throw new ArgumentNullException(nameof(storeContext));
            }
            _cache = cache;
            _userService = userService;
            _productService = productService;
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _orderService.GetAll(this.AuthenticatedUserId());
            return Ok(users);
        }

        [HttpGet("/orders/search")]
        public IActionResult Search([FromQuery(Name = "orderid")] int orderId, [FromQuery(Name = "from")] string from, [FromQuery(Name = "to")] string to)
        {
            Console.WriteLine($"OrdersController.Search({orderId}, {from}, {to}) -------------------------------");
            if (orderId > 0) 
            {
                var order = _orderService.SearchByOrderId(this.AuthenticatedUserId(), orderId);
                if (order == null)
                {
                    Console.WriteLine($"GetByOrderId({orderId}): match not found");
                    return NotFound(new { message = "Invalid order id" });
                }
                return Ok(new List<Order>() {order});
            }

            DateTimeHelper.ParseDateRange(from, to, out DateTime fromDate, out DateTime toDate);

            var orders = _orderService.SearchByDateRange(this.AuthenticatedUserId(), fromDate.ToUniversalTime(), toDate.ToUniversalTime());
            return Ok(orders);
        }

        [Authorize]
        [HttpGet("/orders/{guid}/submit")]
        public IActionResult SubmitCart(string guid)
        {
            Console.WriteLine($"{nameof(SubmitCart)} -------------------------------");

            if(string.IsNullOrWhiteSpace(guid))
            {
                Console.WriteLine($"SubmitCart({guid??""}): guid is null or empty");
                return NotFound(new { message = "Invalid cart guid" });
            }

            if(!_cache.TryGetValue(guid, out Entities.Cart cart))
            {
                Console.WriteLine($"SubmitCart({guid??""}): guid not found in cache");
                return NotFound(new { message = "Invalid cart guid" });
            }

            Entities.Order order = Transformers.CartToOrderTransformer.Transform(cart);
            if (order.Items == null || order.Items.Count(p => p != null) < 1)
            {
                Console.WriteLine("model.UserId < 1");
                return BadRequest(new { message = "At least 1 product is required" });
            }

            if (order.Items.Count(p => p.Quantity < 1) > 1)
            {
                Console.WriteLine("Product found with qty < 1");
                return BadRequest(new { message = "All products must have a quantity of at least 1" });
            }

            if (order.ExTotal < 0)
            {
                Console.WriteLine($"cart.ExTotal < 0");
                return BadRequest(new { message = "Cart total is invalid" });
            }

            // get the login in user
            int userId = this.AuthenticatedUserId();
            var user = _userService.GetById(userId);
            if (user == null)
            {
                Console.WriteLine("match not found");
                return NotFound(new { message = "User not found" });
            }
            order.UserId = userId;

            Console.WriteLine($"UserId: {order.UserId}");

            var uniqueProductIds = order.Items.Select(p => p.ProductId).Distinct().ToList();
            var products = _productService.GetIn(uniqueProductIds);
            if(uniqueProductIds.Count != products.Count())
            {
                Console.WriteLine("not all products were found");
                return NotFound(new { message = "Products not found" });
            }

            Console.WriteLine("_orderService.Save(order);");
            int saveId = _orderService.Save(order);

            Console.WriteLine($"order.id: {order.Id}");

            if(order.Id < 1)
            {
                Console.WriteLine("order.Id < 1. failed to save order");
                return BadRequest(new { message = "Failed to save order. Please try again" });
            }

            Console.WriteLine("success");
            _cache.Remove(guid);

            return Ok(order);

        } // SubmitCart()

        private int AuthenticatedUserId()
        {
            Console.WriteLine("OrdersController.UserId()");
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userIdStr = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            Console.WriteLine($"userIdStr: {userIdStr??""}");
            Int32.TryParse(userIdStr, out int userId);
            Console.WriteLine($"userId: {userId}");
            return userId;
        }

    }
}
