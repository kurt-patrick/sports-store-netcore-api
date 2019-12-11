using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStoreApi.Entities
{
    public class Cart
    {
        public string Guid { get; set; }
        public decimal ExTotal { get; set; }
        public decimal IncTotal { get; set; }
        public decimal Gst { get; set; }
        public int QuantityTotal { get; set; }
        public List<IItemBase> Items {get; set; } = new List<IItemBase>();

        public Cart() : this(System.Guid.NewGuid().ToString())
        {
        }

        internal Cart(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid) || guid.Trim().Length != 36) {
                throw new ArgumentException("Invalid guid");
            }
            this.Guid = guid.Trim();
        }

        public void AddItem(IItemBase item)
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
            this.QuantityTotal = this.Items.Sum(item => item.Quantity);
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