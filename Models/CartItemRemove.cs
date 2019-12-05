using System.ComponentModel.DataAnnotations;

namespace SportsStoreApi.Models
{
    public class CartItemRemove
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Valid product id is required")]
        public int ProductId { get; set; }
    }
}