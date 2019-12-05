using Microsoft.AspNetCore.Mvc;
using System;
using SportsStoreApi.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace SportsStoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private const string CACHE_KEY_CART = "cart";
        private readonly IMemoryCache _cache;
        private readonly IProductService _productService;

        public CartController(IProductService productService, IMemoryCache cache)
        {
            _cache = cache;
            _productService = productService;
        }

        private Entities.Cart GetCartFromCache()
        {
            var cart = _cache.GetOrCreate(CACHE_KEY_CART, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4);
                return new Entities.Cart();
            });
            return cart;
        }

        private void SaveCartToCache(Entities.Cart cart)
        {
            _cache.Set(CACHE_KEY_CART, cart);
        }

        [HttpPost("/cart/add")]
        public IActionResult AddItem([FromBody]Models.CartItemAdd model)
        {
            Console.WriteLine($"AddItem(productId: {model.ProductId}, qty: {model.Quantity}) -------------------------------");
            var product = _productService.GetById(model.ProductId);
            if (product == null)
            {
                Console.WriteLine("GetById(): match not found");
                return BadRequest(new { message = "Product not found: " + model.ProductId });
            }

            var cart = GetCartFromCache();
            var cartItem = Transformers.CartItemAddToCartItem.Transform(model, product);
            cart.AddItem(cartItem);
            SaveCartToCache(cart);

            Console.WriteLine("AddItem(): success");
            return Ok(cart);
        }

        [HttpPost("/cart/remove")]
        public IActionResult RemoveItem([FromBody]Models.CartItemRemove model)
        {
            Console.WriteLine($"RemoveItem(productId: {model.ProductId}) -------------------------------");
            var cart = GetCartFromCache();
            cart.RemoveItem(model.ProductId);
            SaveCartToCache(cart);
            Console.WriteLine("RemoveItem(): success");
            return Ok(cart);
        }

        [HttpGet("/cart")]
        public IActionResult GetCart()
        {
            var cart = GetCartFromCache();
            return Ok(cart);
        }

        [HttpGet("/cart/clear")]
        public IActionResult ClearCart()
        {
            var cart = GetCartFromCache();
            cart.ClearCart();
            SaveCartToCache(cart);
            return Ok(cart);
        }

    }
}
