using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obooking.Models
{
    public class Cart
    {
        public Order order { get; set; }
        public Book book { get; set; }
        public OrderDetails orderDetails { get; set; }

    }
}