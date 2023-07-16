using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obooking.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string status { get; set; }
        // Navigation property
        public ICollection<OrderDetails> orderDetails { get; set; }

    }
}