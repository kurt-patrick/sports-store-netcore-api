using SportsStoreApi.Entities;
using SportsStoreApi.Models;

namespace SportsStoreApi.Transformers
{
    public static class CartToOrderTransformer
    {
        public static Entities.Order Transform(Cart cart)
        {
            var order = new Entities.Order();
            order.ExTotal = cart.ExTotal;
            order.Gst = cart.Gst;
            order.IncTotal = cart.IncTotal;
            order.QuantityTotal = cart.QuantityTotal;
            cart.Items.ForEach(item => order.Items.Add(new OrderItem(item)));
            return order;
        }
        
    }
}