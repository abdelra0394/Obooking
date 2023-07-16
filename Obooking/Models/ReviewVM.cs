using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obooking.Models
{
    public class ReviewVM
    {
        public Review review { get; set; }
        public Book book { get; set; }
        public User user { get; set; }
    }
}