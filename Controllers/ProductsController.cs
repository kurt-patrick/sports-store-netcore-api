using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportsStoreApi.Services;
using SportsStoreApi.Models;
using System.Linq;
using System;
using SportsStoreApi.Interfaces;

namespace SportsStoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //[AllowAnonymous]
        //[HttpPost("authenticate")]
        [HttpGet("/products/{id}")]
        public IActionResult Get(int id)
        {
            Console.WriteLine($"ProductsController.Get({id}) -------------------------------");
            var product = _productService.GetById(id);

            if (product == null)
            {
                Console.WriteLine("match not found");
                return NotFound(id);
            }

            Console.WriteLine("Get(): success");
            return Ok(product);
        }

        [HttpGet("/products/search")]
        public IActionResult Search([FromQuery(Name = "name")] string name, [FromQuery(Name = "desc")] string desc)
        {
            Console.WriteLine($"ProductsController.Search({name}) -------------------------------");
            var products = _productService.Search(name);
            return Ok(products);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }
    }
}
