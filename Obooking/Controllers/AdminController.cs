using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Obooking.Models;


namespace Obooking.Controllers
{
    public class AdminController : Controller
    {
        [DataContract]
        public class DataPoint
        {
            public DataPoint(double x, double y)
            {
                this.X = x;
                this.Y = y;
            }

            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "x")]
            public Nullable<double> X = null;

            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "y")]
            public Nullable<double> Y = null;
        }
        private ApplicationDbContext _context;
        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin
        public ActionResult Show()
        {
            return View();
        }


        // Category
        [HttpGet]
        public ActionResult createCategory()
        {
            return View();
        }



        [HttpPost]
        public ActionResult createCategory(Category category)
        {

            _context.Categories_table.Add(category);
            _context.SaveChanges();

            return View();
        }
        public ActionResult ShowCategories()
        {
            var lis = _context.Categories_table.ToList();
            return View(lis);
        }
        public ActionResult DeleteCategory(int id)
        {

            var cate = _context.Categories_table.SingleOrDefault(m => m.CategoryID == id);
            var ListOfBooks = _context.Books_table.ToList();
            foreach (var x in ListOfBooks)
            {
                if (x.BookCategory == cate.CategoryName)
                {
                    _context.Books_table.Remove(x);

                }
            }

            _context.Categories_table.Remove(cate);
            _context.SaveChanges();
            return RedirectToAction("ShowCategories", "Admin");
        }


        //Book 
        [HttpGet]
        public ActionResult createBook()
        {
            Book b1 = new Book(1);

          
            
            Book b2 = b1.GetClone();

            var category = _context.Categories_table.ToList();
            var categories = new List<string>();
            foreach (var item in category)
                categories.Add(item.CategoryName);
            ViewBag.Categories = new SelectList(categories);

            var statuslist = new List<string> { "available", "about to end", "not available" };
            ViewBag.status = new SelectList(statuslist);

            var popularit = new List<string> { "Famous", "Common", "Unknown" };
            ViewBag.popular = new SelectList(popularit);

            return View(b2);
        }

        [HttpPost]
        public ActionResult createBook(Book book)
        {
            var category = _context.Categories_table.ToList();
            var categories = new List<string>();
            foreach (var item in category)
                categories.Add(item.CategoryName);
            ViewBag.Categories = new SelectList(categories);

            var statuslist = new List<string> { "available", "about to end", "not available" };
            ViewBag.status = new SelectList(statuslist);

            var popularit = new List<string> { "Famous", "Common", "Unknown" };
            ViewBag.popular = new SelectList(popularit);

            string fileName;
            string extension;

            if (book.BookId == 0)
            {
                fileName = Path.GetFileNameWithoutExtension(book.ImageFile.FileName);
                extension = Path.GetExtension(book.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                book.ImageBook = "../Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
                book.ImageFile.SaveAs(fileName);

                if (book.BookCount > 0 && book.BookCount < 10)
                {
                    book.BookStatus = "about to end";
                }
                else if (book.BookCount == 0)
                {
                    book.BookStatus = "not available";
                }
                else
                {
                    book.BookStatus = "available";
                }

                _context.Books_table.Add(book);
            }
            else
            {
                var booknew = _context.Books_table.Find(book.BookId);

                fileName = Path.GetFileNameWithoutExtension(book.ImageFile.FileName);
                extension = Path.GetExtension(book.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                book.ImageBook = "../Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
                book.ImageFile.SaveAs(fileName);

                booknew.BooKDescription = book.BooKDescription;
                booknew.BookName = book.BookName;
                booknew.BookCategory = book.BookCategory;
                booknew.BookPrice = book.BookPrice;
                booknew.BookCount = book.BookCount;
                booknew.BookStatus = book.BookStatus;
                booknew.BookEdition = book.BookEdition;
                booknew.BookPopularity = book.BookPopularity;
                booknew.ImageBook = book.ImageBook;
                booknew.ImageFile = book.ImageFile;
            }

            _context.SaveChanges();
            ModelState.Clear();
            return View();
        }

        public ActionResult EditBook(int id)
        {
            var category = _context.Categories_table.ToList();
            var categories = new List<string>();
            foreach (var item in category)
                categories.Add(item.CategoryName);
            ViewBag.Categories = new SelectList(categories);

            var statuslist = new List<string> { "available", "about to end", "not available" };
            ViewBag.status = new SelectList(statuslist);

            var popularit = new List<string> { "Famous", "Common", "Unknown" };
            ViewBag.popular = new SelectList(popularit);

            var book = _context.Books_table.Find(id);

            var model = new Book
            {
                BookId = book.BookId,
                BookName = book.BookName,
                BooKDescription = book.BooKDescription,
                BookEdition = book.BookEdition,
                BookCategory = book.BookCategory,
                BookStatus = book.BookStatus,
                BookCount = book.BookCount,
                BookPopularity = book.BookPopularity,
                BookPrice = book.BookPrice,
                ImageBook = book.ImageBook,
                ImageFile = book.ImageFile,
            };

            return View("createBook", model);
        }


        public ActionResult Showbooks()
        {
            var lis = _context.Books_table.ToList();
            return View(lis);
        }

        public ActionResult DeleteBook(int id)
        {
            var book = _context.Books_table.SingleOrDefault(m => m.BookId == id);
            _context.Books_table.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Showbooks", "Admin");
        }

        public ActionResult ManageOrders()
        {
            var orders = _context.Order_table.Where(x=> x.status == "Waiting").ToList();
           
            return View(orders);
        }

        public ActionResult OrderAccept(int id)
        {
            var orderDetails = _context.OrderDetails_table.Where(x => x.OrderID == id).ToList();
            foreach (var item in orderDetails)
            {
                item.status = "Admin Approved";
                //var book = _context.Books_table.Where(x => x.BookId == item.BookID).SingleOrDefault();
                //book.BookCount -= item.quantity;
            }
            var order = _context.Order_table.Where(x => x.ID == id).SingleOrDefault();
           
            order.status = "Admin Approved";
            _context.SaveChanges();
            TempData["AlertMessage"] = "Order Accepted...";
            return RedirectToAction("ManageOrders","Admin");
        }

        public ActionResult OrderDelete(int id)
        {
            var orderDetails = _context.OrderDetails_table.Where(x => x.OrderID == id).ToList();
            foreach (var item in orderDetails)
            {
                item.status = "rejected";
            }
            var order = _context.Order_table.Where(x => x.ID == id).SingleOrDefault();
            order.status = "rejected";
            _context.SaveChanges();
            TempData["AlertMessage"] = "Order Rejected...";

            return RedirectToAction("ManageOrders", "Admin");
        }

        public ActionResult OrderDetails(int id)
        {

           // var orderDetails = _context.OrderDetails_table.Where(x => x.OrderID == id).ToList();
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

        public ActionResult ShowSalesData()
        {
           
            List<string> labels = _context.Books_table.Select(p => p.BookName).ToList();
            List<int> SalesNumber = _context.Books_table.Select(p => p.number_sold).ToList();
            ViewBag.labels = JsonConvert.SerializeObject(labels);
            ViewBag.SalesNumber = JsonConvert.SerializeObject(SalesNumber);

            //ViewBag.Result = JsonConvert.SerializeObject(Result);
            return View();
        }


        [HttpPost]
        public List<object> GetSalesData()
        {
            List<object> data = new List<object>();

            List<string> lables = _context.Books_table.Select(p => p.BookName).ToList();
            data.Add(lables);

            List<int> SalesNumber = _context.Books_table.Select(p => p.number_sold).ToList();
            data.Add(SalesNumber);

            return data;

        }
        public ActionResult chart2()
        {
            List<string> labels = _context.Books_table.Select(p => p.BookCategory).ToList();
            List<int> SalesNumber = _context.Books_table.Select(p => p.number_sold).ToList();
            ViewBag.labels = JsonConvert.SerializeObject(labels);
            ViewBag.SalesNumber = JsonConvert.SerializeObject(SalesNumber);

            return View();
        }

    }
}