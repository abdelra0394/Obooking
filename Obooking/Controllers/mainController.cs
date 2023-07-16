using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obooking.Models;
using Obooking.MyClasses;

namespace Obooking.Controllers
{
    public class mainController : Controller
    {
        public ApplicationDbContext _context;
        MySession session;
        public mainController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        // GET: main
        public ActionResult Showmain(String sortOrder,string searchString)
        {
            ViewData["PopularitySortParam"] = String.IsNullOrEmpty(sortOrder) ? "Popularity_asce" : "";
            ViewData["currentFilter"] = searchString;
            var books = from x in _context.Books_table select x;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(x => x.BookCategory == searchString);
            }

            switch (sortOrder)
            {
                case "Popularity_asce":
                    books = books.OrderBy(x => x.BookPopularity);
                    break;

                default:
                    break;
            }
            return View(books.ToList());
        }

        public ActionResult ViewReview(int id)
        {
          //  var reviews = _context.Review_table.Where(x => x.bookid == id).ToList();

            var review = _context.Review_table.ToList();
            var user = _context.my_users.ToList();
            var book = _context.Books_table.ToList();

            var output = from r in review
                         join u in user on r.userid equals u.id
                         join b in book on r.Book_Name equals b.BookName
                         where b.BookId == id
                         select new ReviewVM
                         {
                             book = b,
                             review = r,
                             user = u
                         };


            return View(output);
        }

        [HttpGet]
        public ActionResult Review()
        {
            session = MySession.Instance;
            var order = _context.Order_table.ToList();
            var order_details = _context.OrderDetails_table.ToList();
            var book = _context.Books_table.ToList();
            var output = from o in order
                         join d in order_details on o.ID equals d.OrderID
                         join b in book on d.BookID equals b.BookId
                         where o.UserName == session.username && d.status == "sold"
                         select new Cart
                         {
                             book = b,
                             order = o,
                             orderDetails = d
                         };
            ///
            var books_name = new List<string>();
            foreach (var item in output)
                books_name.Add(item.book.BookName);
            ViewBag.BooksNames = new SelectList(books_name);

            if (books_name.Count == 0)
            {
                ViewBag.check = "you have to pursher anybook";
            }
            else
            {

                ViewBag.check = "Select Your book...";
            }

            return View();
        }

        [HttpPost]
        public ActionResult Review(Review rev)
        {
            session = MySession.Instance;
            var order = _context.Order_table.ToList();
            var order_details = _context.OrderDetails_table.ToList();
            var book = _context.Books_table.ToList();
            var output = from o in order
                         join d in order_details on o.ID equals d.OrderID
                         join b in book on d.BookID equals b.BookId
                         where o.UserName == session.username && d.status == "sold"
                         select new Cart
                         {
                             book = b,
                             order = o,
                             orderDetails = d
                         };
            ///
            var books_name = new List<string>();
            foreach (var item in output)
                books_name.Add(item.book.BookName);
            ViewBag.BooksNames = new SelectList(books_name);
            if (books_name.Count == 0)
            {
                ViewBag.check = "you have to pursher anybook";
            }
            else
            {

                ViewBag.check = "Select Your book...";
            }
            rev.userid = session.id;
            _context.Review_table.Add(rev);
            _context.SaveChanges();
            return View();
        }
    }
}