using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obooking.Models
{
    public class MakeOrder
    {
        public OrderDetails OrderDetails { get; set; }
        public Book book { get; set; }

    }
}