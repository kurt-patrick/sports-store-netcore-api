using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsStoreApi.Models
{
    public class OrderSubmission
    {
        public string OrderId => System.Guid.NewGuid().ToString();

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Valid user id required")]
        public int UserId { get; set; }

        [Required]
        public List<Product> Products { get; set; } = new List<Product>();

        public class Product
        {
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Valid id required")]
            public int Id { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "At least 1 quantity is required")]
            public int Quantity { get; set; }
        }

    }


}