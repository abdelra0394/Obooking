using Obooking.DesignPatterns.Proxy;
using Obooking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Obooking.MyClasses.Proxy
{
    public class CreateAccReal : Create
    {
        private string user_name;
        public CreateAccReal(String user_name)
        {
            this.user_name = user_name;
        }
        public String create()
        {
            if (user_name != "")
            {
                return "Account Created";
            }
            else
            {
                return "username is used...";
            }

        }

    }
}