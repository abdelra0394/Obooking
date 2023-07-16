using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obooking.Models
{
    public class OrderDetails
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int BookID { get; set; }
        public int quantity { get; set; }
        public string  status { get; set; }


        //navigation property
        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}