using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obooking.DesignPatterns.Proxy;
using Obooking.Models;
using Obooking.MyClasses;
using Obooking.MyClasses.Proxy;

namespace Obooking.Controllers
{
    public class userController : Controller
    {
        public ApplicationDbContext _context;

        public userController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        // GET: user

        public ActionResult create()
        {
            return View();
        }
        public ActionResult signup(User user)
        {
            var list = _context.my_users.ToList();
            List<string> users = new List<string>();
            foreach (var item in list)
            {
                users.Add(item.username.ToString());
            }

            Create create = new ProxyCreateAcc(user.username.ToString(), users);

            String showing_alert = create.create();
            if (showing_alert.Equals("Account Created"))
            {
                TempData["AlertMessageSignup"] = showing_alert;
                _context.my_users.Add(user);
            }
            else
                TempData["AlertMessageSignup"] = showing_alert;

            _context.SaveChanges();
            return RedirectToAction("create");
        }
        public ActionResult login()
        {
         
            return View();
        }
        public ActionResult loginlogic(User user)
        {
            //var list = _context.users.ToList();
            Console.WriteLine(user.username);
            //var lg = _context.users.Where(m => m.username.Equals(user.username) && m.password.Equals(user.password));
            if (user.username.ToString().Equals("admin"))
            {
                if (user.password.ToString().Equals("admin"))
                {
                  
                    return RedirectToAction("Show", "Admin");
                }
            }
            var users = _context.my_users.ToList();
            foreach(var x in users)
            {
                if (x.username.ToString().Equals(user.username.ToString()))
                {
                    if (x.password.ToString().Equals(user.password.ToString()))
                    {
                        MySession session = MySession.Instance;
                        session.username = x.username;
                        session.address = x.addresss;
                        session.id = x.id;
                        return RedirectToAction("PrevOrders", "Order");
                    }
                }
            }
            return RedirectToAction("login", "user");

        }
       
    }
}