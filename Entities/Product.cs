namespace SportsStoreApi.Entities
{
    public enum Gender {
        Mens = 0,
        Womens = 1
    }

    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ImageUrl { get; set; }
        public Gender Gender { get; set; }
        public string ColourDescription { get; set; }
    }

}