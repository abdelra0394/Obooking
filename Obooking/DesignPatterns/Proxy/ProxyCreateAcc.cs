using Obooking.DesignPatterns.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obooking.MyClasses.Proxy
{
    public class ProxyCreateAcc : Create
    {
        private string user_name;
        List<string> users;

        private CreateAccReal createAccReal;

        private ProxyCreateAcc() { }

        public ProxyCreateAcc(String user_name, List<string> users)
        {
            this.user_name = user_name;
            this.users = users;
        }

        public String create()
        {
            foreach (var item in users)
            {
                if (user_name.Equals(item))
                {
                    user_name = "";
                }
            }
            createAccReal = new CreateAccReal(user_name);
            return createAccReal.create();
        }

    }
}