using Microsoft.AspNetCore.Mvc;
using System;
using SportsStoreApi.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SportsStoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public CartController(IProductService productService, IUserService userService, IMemoryCache cache)
        {
            _cache = cache;
            _productService = productService;
            _userService = userService;
        }

        private Entities.Cart GetCartFromCache(string guid)
        {
            var retVal = _cache.GetOrCreate(guid, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4);
                return new Entities.Cart(guid);
            });
            return retVal;
        }

        private void SaveCartToCache(Entities.Cart cart) => _cache.Set(cart.Guid, cart);

        [HttpPost("/cart/{guid}/add")]
        public IActionResult AddItem([FromBody]Models.CartItemAdd model, string guid)
        {
            Console.WriteLine($"AddItem(productId: {model.ProductId}, qty: {model.Quantity}, guid: {guid??""}) -------------------------------");
            var product = _productService.GetById(model.ProductId);
            if (product == null)
            {
                Console.WriteLine("GetById(): match not found");
                return BadRequest(new { message = "Product not found: " + model.ProductId });
            }

            var cart = GetCartFromCache(guid);
            var cartItem = Transformers.CartItemAddToCartItem.Transform(model, product);
            cart.AddItem(cartItem);
            SaveCartToCache(cart);

            Console.WriteLine("AddItem(): success");
            return Ok(cart);
        }

        [HttpPost("/cart/{guid}/remove")]
        public IActionResult RemoveItem([FromBody]Models.CartItemRemove model, string guid)
        {
            Console.WriteLine($"RemoveItem(productId: {model.ProductId}, guid: {guid??""}) -------------------------------");
            var cart = GetCartFromCache(guid);
            cart.RemoveItem(model.ProductId);
            SaveCartToCache(cart);
            Console.WriteLine("RemoveItem(): success");
            return Ok(cart);
        }

        [HttpGet("/cart/create")]
        public IActionResult CreateCart()
        {
            var cart = new Entities.Cart();
            SaveCartToCache(cart);
            return Ok(cart);
        }

        [HttpGet("/cart/{guid}")]
        public IActionResult GetCart(string guid)
        {
            var cart = GetCartFromCache(guid);
            return Ok(cart);
        }

        [HttpGet("/cart/{guid}/clear")]
        public IActionResult ClearCart(string guid)
        {
            var cart = GetCartFromCache(guid);
            cart.ClearCart();
            SaveCartToCache(cart);
            return Ok(cart);
        }

    }
}
