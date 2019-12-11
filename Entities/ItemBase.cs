namespace SportsStoreApi.Entities
{
    public interface IItemBase
    {
        decimal ExPrice { get; set; }
        decimal IncPrice { get; set; }
        decimal Gst { get; set; }
        decimal ExTotal { get; set; }
        decimal IncTotal { get; set; }
        decimal GstTotal { get; set; }
        int ProductId { get; set; }
        string ProductName { get; set; }
        int Quantity { get; set; }
        string ImageUrl { get; set; }
        string Description { get; set; }
    }

    public class CartItem : IItemBase
    {
        public decimal ExPrice { get; set; }
        public decimal IncPrice { get; set; }
        public decimal Gst { get; set; }
        public decimal ExTotal { get; set; }
        public decimal IncTotal { get; set; }
        public decimal GstTotal { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}