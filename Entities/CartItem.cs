namespace SportsStoreApi.Entities
{
    public class CartItem
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