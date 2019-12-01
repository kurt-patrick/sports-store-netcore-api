using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportsStoreApi.Services;
using SportsStoreApi.Models;
using System.Linq;
using System;
using SportsStoreApi.Interfaces;

namespace SportsStoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private IUserService _userService;
        private IProductService _productService;
        private IOrderService _orderService;

        public OrdersController(IUserService userService, IProductService productService, IOrderService orderService)
        {
            _userService = userService;
            _productService = productService;
            _orderService = orderService;
        }

        [HttpPost("submit")]
        public IActionResult Submit([FromBody]OrderSubmission model)
        {
            Console.WriteLine($"{nameof(Submit)} -------------------------------");
            Console.WriteLine($"UserId: {model.UserId}");

            if (model.Products == null || model.Products.Count(p => p != null) < 1)
            {
                Console.WriteLine("model.UserId < 1");
                return BadRequest(new { message = "At least 1 product is required" });
            }

            if (model.Products.Count(p => p.Quantity < 1) > 1)
            {
                Console.WriteLine("Product found with qty < 1");
                return BadRequest(new { message = "All products must have a quantity of at least 1" });
            }

            var user = _userService.GetById(model.UserId);
            if (user == null)
            {
                Console.WriteLine("match not found");
                return BadRequest(new { message = "User not found" });
            }

            var uniqueProductIds = model.Products.Select(p => p.Id).Distinct().ToList();
            var products = _productService.GetIn(uniqueProductIds);
            if(uniqueProductIds.Count != products.Count())
            {
                Console.WriteLine("not all products were found");
                return BadRequest(new { message = "not all products were found" });
            }

            Console.WriteLine("_orderService.Save(model);");
            var newOrder = _orderService.Save(model);

            if(string.IsNullOrWhiteSpace(newOrder.OrderId))
            {
                Console.WriteLine("newOrder.OrderId is empty. failed to save");
                return BadRequest(new { message = "Failed to save order. Please try again" });
            }

            Console.WriteLine("success");
            return Ok(newOrder);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
        
    }
}
