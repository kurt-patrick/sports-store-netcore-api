using System.ComponentModel.DataAnnotations;

namespace SportsStoreApi.Entities
{
    public class OrderItem : IItemBase
    {
        public int Id { get; set; }
        public decimal ExPrice { get; set; }
        public decimal IncPrice { get; set; }
        public decimal Gst { get; set; }
        public decimal ExTotal { get; set; }
        public decimal IncTotal { get; set; }
        public decimal GstTotal { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Valid ProductId required")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public OrderItem()
        {
        }

        public OrderItem(IItemBase value)
        {
            if(value == null)
            {
                throw new System.ArgumentNullException(nameof(value));
            }
            this.Description = value.Description;
            this.ExPrice = value.ExPrice;
            this.ExTotal = value.ExTotal;
            this.Gst = value.Gst;
            this.GstTotal = value.GstTotal;
            this.ImageUrl = value.ImageUrl;
            this.IncPrice = value.IncPrice;
            this.IncTotal = value.IncTotal;
            this.ProductId = value.ProductId;
            this.ProductName = value.ProductName;
            this.Quantity = value.Quantity;
        }
    }
}