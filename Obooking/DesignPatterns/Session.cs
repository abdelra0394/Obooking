using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obooking.MyClasses
{
    //this will be a singleton class
    public sealed class MySession
    {
        public int id { get; set; }
        public string  username { get; set; }
        public string address { get; set; }


        private MySession() { }
        private static readonly object lock_obj = new object ();  
        private static MySession instance = null;
        public static MySession Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lock_obj)
                        {
                            if (instance == null)
                            {
                                instance = new MySession();
                            }
                        }
                }
                return instance;
            }
        }

        public int Property
        {
            get => default;
            set
            {
            }
        }
    }
}