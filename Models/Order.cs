using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsStoreApi.Models
{
    public class Order : OrderSubmission
    {
        public string OrderId {get; set;}

        public Order() : base() 
        {
            this.OrderId = System.Guid.NewGuid().ToString();
        }

    }


}