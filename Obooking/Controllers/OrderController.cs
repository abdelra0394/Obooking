using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obooking.Models;
using Obooking.MyClasses;


namespace Obooking.Controllers
{
  
    public class OrderController : Controller
    {
        private ApplicationDbContext _context;
        MySession session;
        
        public OrderController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        //Get
        public ActionResult Create(int? id)
        {
             var book = _context.Books_table.Find(id);
            //var book = _context.Books_table.ToList();
            var makeorder = new MakeOrder();
            makeorder.book = book;
            return View(makeorder);
        }

        //post
        [HttpPost]
        public ActionResult Create(MakeOrder makeOrder)
        {
            session = MySession.Instance;

           
            var order = _context.Order_table.SingleOrDefault(x => x.UserName.Equals(session.username) && x.status =="Waiting");
            //Order neworder;
            if (order == null )
            {
                order = new Order();
                order.UserName = session.username;
                order.status = "Waiting";
                _context.Order_table.Add(order);
                _context.SaveChanges();
            }
            else {

                OrderDetails orderDetails = new OrderDetails();
                orderDetails.quantity = makeOrder.OrderDetails.quantity;
                orderDetails.BookID = makeOrder.book.BookId;
                orderDetails.OrderID = order.ID;
                orderDetails.status = "Waiting";

                //this code is to minus the count
                //var book = _context.Books_table.Where(b => b.BookId == makeOrder.book.BookId).FirstOrDefault();
                //book.BookCount -= orderDetails.quantity;
                
                _context.OrderDetails_table.Add(orderDetails);
                _context.SaveChanges();
            }

            


            return RedirectToAction("Showmain", "main");
        }

        // called to redirect to cart
        public ActionResult userOrder()
        {
            session = MySession.Instance;
            var order = _context.Order_table.SingleOrDefault(x => x.UserName.Equals(session.username) && x.status =="Waiting");
            
            return RedirectToAction("showCart","Order", new { id = order.ID });
        }

        public ActionResult showCart(int? id)
        {
            //var orderDetails = _context.OrderDetails_table.Where(x => x.OrderID == id);
            var order = _context.Order_table.ToList();
            var order_details = _context.OrderDetails_table.ToList();
            var book = _context.Books_table.ToList();

            var output = from o in order
                         join d in order_details on o.ID equals d.OrderID
                         join b in book on d.BookID equals b.BookId
                         where o.ID == id
                         select new Cart
                         {
                             book = b,
                             order=o,
                             orderDetails =d
                         };
           // string x = output.FirstOrDefault().orderDetails.status;
            //Console.WriteLine(x);
            return View(output);
        }

        public ActionResult EditOrder(int? id)
        {
            var orderDetails = _context.OrderDetails_table.Where(x => x.ID == id).SingleOrDefault();
            int bookid = orderDetails.BookID;
            if (orderDetails != null)
            {
                _context.OrderDetails_table.Remove(orderDetails);
                _context.SaveChanges();

            }
            return RedirectToAction("Create", "Order", new { id = bookid});
        }

        public ActionResult DeleteOrder(int? id)
        {
            var orderDetails = _context.OrderDetails_table.Where(x => x.ID == id).SingleOrDefault();
            if (orderDetails != null)
            {
                _context.OrderDetails_table.Remove(orderDetails);
                _context.SaveChanges();

            }
           
            //vm.user.id;
            //var userr = _context.my_users.Where(x => x.username.Equals(username)).SingleOrDefault();
             
            return RedirectToAction("userorder","Order");
            
        }

        public ActionResult Payment()
        {
            session = MySession.Instance;
            var order = _context.Order_table.SingleOrDefault(x => x.UserName.Equals(session.username) && x.status=="Admin Approved");
            if(order != null)
                return RedirectToAction("ShowBill", "Order", new { id = order.ID });
            else
                return RedirectToAction("ShowBill", "Order", null);
        }

        public ActionResult ShowBill(int? id)
        {
            var order = _context.Order_table.ToList();
            var order_details = _context.OrderDetails_table.ToList();
            var book = _context.Books_table.ToList();
            ViewBag.orderid = id;
            var output = from o in order
                         join d in order_details on o.ID equals d.OrderID
                         join b in book on d.BookID equals b.BookId
                         where o.ID == id && d.status =="Admin Approved"
                         select new Cart
                         {
                             book = b,
                             order = o,
                             orderDetails = d
                         };
            double result = 0;
            foreach (var item in output)
            {
                result += item.book.BookPrice * item.orderDetails.quantity;
            }
            ViewBag.result = result;
            return View(output);
        }
        
        public ActionResult ConfirmPayment(int id)
        {
            var orderDetails = _context.OrderDetails_table.Where(x => x.OrderID == id).ToList();
            foreach (var item in orderDetails)
            {
                item.status = "sold";
                var book = _context.Books_table.Where(x => x.BookId == item.BookID).SingleOrDefault();
                book.number_sold++;
                book.BookCount -= item.quantity;
            }
            var order = _context.Order_table.Where(x => x.ID == id).SingleOrDefault();

            order.status = "sold";
            _context.SaveChanges();
            return RedirectToAction("ShowBill", "Order", new { id = id });
        }

        public ActionResult OrderStatus()
        {
            session = MySession.Instance;
            
            var orders = _context.Order_table.Where(x => x.UserName == session.username && x.status !="sold").ToList();
           /* var order = from x in orders
                        where x.status != "sold"
                        select new Order
                        {
                            status = x.status,
                            UserName = x.UserName
                        };*/
            return View(orders);
        }
        public ActionResult OrderDelete(int id)
        {
            var order = _context.Order_table.SingleOrDefault(x => x.ID == id);
            int idd = id;
            _context.Order_table.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("OrderStatus", "Order");
        }

        public ActionResult PrevOrders()
        {
            session = MySession.Instance;
            var orders = _context.Order_table.Where(x => x.status == "sold" && x.UserName == session.username).ToList();

            return View(orders);
           
        }
        public ActionResult OrderDetails(int id)
        {
            var order = _context.Order_table.ToList();
            var order_details = _context.OrderDetails_table.ToList();
            var book = _context.Books_table.ToList();

            var output = from o in order
                         join d in order_details on o.ID equals d.OrderID
                         join b in book on d.BookID equals b.BookId
                         where o.ID == id
                         select new Cart
                         {
                             book = b,
                             order = o,
                             orderDetails = d
                         };
            return View(output);
        }

    }
}