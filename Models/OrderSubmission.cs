using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsStoreApi.Models
{
    public class OrderSubmission
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public List<Product> Products { get; set; } = new List<Product>();

        public class Product
        {
            [Required]
            public int Id { get; set; }

            [Required]
            public int Quantity { get; set; }
        }

    }


}