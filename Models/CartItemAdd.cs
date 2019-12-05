using System.ComponentModel.DataAnnotations;

namespace SportsStoreApi.Models
{
    public class CartItemAdd
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Valid product id is required")]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "At least 1 quantity is required")]
        public int Quantity { get; set; }
    }
}