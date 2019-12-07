using System.Collections.Generic;
using System.Linq;

namespace SportsStoreApi.Entities
{
    public class Cart
    {
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public int Id { get; set; }
        public decimal ExTotal { get; set; }
        public decimal IncTotal { get; set; }
        public decimal Gst { get; set; }
        public List<CartItem> Items {get; set; } = new List<CartItem>();

        public void AddItem(CartItem item)
        {
            int index = this.IndexOfItem(item.ProductId);
            if(index == -1)
            {
                this.Items.Add(item);
            }
            else 
            {
                this.Items[index] = item;
            }
            this.CalculateCartTotals();
        }

        public void RemoveItem(int productId)
        {
            int index = this.IndexOfItem(productId);
            if(index >= 0)
            {
                this.Items.RemoveAt(index);
            }
            this.CalculateCartTotals();
        }

        public void ClearCart()
        {
            this.Items.Clear();
            this.CalculateCartTotals();
        }

        private void CalculateCartTotals()
        {
            this.ExTotal = this.Items.Sum(item => item.ExTotal);
            this.IncTotal = this.Items.Sum(item => item.IncTotal);
            this.Gst = this.Items.Sum(item => item.GstTotal);
        }

        private int IndexOfItem(int productId)
        {
            int index = -1;
            for(int i=0; i<this.Items.Count; i++)
            {
                if(this.Items[i].ProductId == productId)
                {
                    return i;
                }
            }
            return index;
        }

        public override string ToString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}