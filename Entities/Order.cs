using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SportsStoreApi.Entities
{
    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Valid user id required")]
        public int UserId { get; set; }
        public decimal ExTotal { get; set; }
        public decimal IncTotal { get; set; }
        public decimal Gst { get; set; }
        public int QuantityTotal { get; set; }
        public List<OrderItem> Items {get; set; } = new List<OrderItem>();

        public Order()
        {
        }

        public override string ToString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}