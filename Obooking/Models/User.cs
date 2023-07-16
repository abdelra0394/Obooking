using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obooking.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string addresss { get; set; }
        public string phone_number { get; set; }
    }
}