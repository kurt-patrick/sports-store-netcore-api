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

        [Authorize]
        [HttpGet("/cart/{guid}/submit")]
        public IActionResult SubmitCart(string guid)
        {
            if(string.IsNullOrWhiteSpace(guid))
            {
                Console.WriteLine($"SubmitCart({guid??""}): guid is null or empty");
                return BadRequest(new { message = "Invalid cart guid" });
            }

            if(!_cache.TryGetValue(guid, out Entities.Cart cart))
            {
                Console.WriteLine($"SubmitCart({guid??""}): guid not found in cache");
                return BadRequest(new { message = "Invalid cart guid" });
            }

            if (cart.Items.Count < 1)
            {
                Console.WriteLine($"SubmitCart({guid}): item count < 1");
                return BadRequest(new { message = "Cart must contain at least 1 item" });
            }
            if (cart.ExTotal < 0)
            {
                Console.WriteLine($"SubmitCart({guid}): cart.ExTotal < 0");
                return BadRequest(new { message = "Cart total is invalid" });
            }

            // get the login in user
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userIdStr = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            Console.WriteLine($"userIdStr: {userIdStr??""}");
            Int32.TryParse(userIdStr, out int userId);
            Console.WriteLine($"userId: {userId}");

            var user = _userService.GetById(userId);
            if(user == null) 
            {
                return BadRequest(new { message = "Unknown user associated to cart" });
            }

            Entities.Order order = Transformers.CartToOrderTransformer.Transform(cart);
            order.UserId = userId;

            // SaveCartToCache(cart);
            return Ok(order);
        }


    }
}
