using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obooking.Models
{
    public class CartVM
    {
        public Order order { get; set; }
        public User user { get; set; }
        public OrderDetails orderDetails { get; set; }
    }
}