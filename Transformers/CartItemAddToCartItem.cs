using SportsStoreApi.Entities;
using SportsStoreApi.Models;

namespace SportsStoreApi.Transformers
{
    public static class CartItemAddToCartItem
    {
        public static Entities.CartItem Transform(CartItemAdd model, Product product)
        {
            var cartItem = new Entities.CartItem();
            cartItem.ExPrice = product.ProductPrice;
            cartItem.ExTotal = cartItem.ExPrice * model.Quantity;
            cartItem.Gst = cartItem.ExPrice * 0.10m;
            cartItem.GstTotal = cartItem.ExTotal * 0.10m;
            cartItem.IncPrice = cartItem.ExPrice + cartItem.Gst;
            cartItem.IncTotal  = cartItem.ExTotal + cartItem.GstTotal;
            cartItem.ProductId = product.Id;
            cartItem.ProductName = product.ProductName;
            cartItem.Quantity = model.Quantity;
            cartItem.Description = product.ColourDescription;
            cartItem.ImageUrl = product.ImageUrl;
            return cartItem;
        }
        
    }
}