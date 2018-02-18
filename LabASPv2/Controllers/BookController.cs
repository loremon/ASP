using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabASPv2.Models;

namespace LabASPv2.Controllers
{
    public class BookController : Controller
    {
        private Db db = new Db();

        public ActionResult Index()
        {
            return RedirectToAction("Books");
        }

        public ActionResult Books(String addition)
        {
            if (addition == null || addition == "")
            {
                // {pageNumber}P{booksOnPage}
                return RedirectToAction("Books", "Book", new { addition = "1P10" });
            }
            List <int> pageAndNumber = addition.Split('P').ToList()
                .ConvertAll(new Converter<String, int>(StringToInt));
            List<Book> books;
            if (pageAndNumber.Count() != 2 || pageAndNumber[0] < 0 || pageAndNumber[1] < 0)
            {
                return RedirectToAction("Books", "Book", new { addition = "1P10" });
            }
            else
            {
                int pageNumber = pageAndNumber[0];
                int booksOnPage = pageAndNumber[1];
                books = db.getBooks((pageNumber - 1) * booksOnPage, pageNumber * booksOnPage - 1); 
            }
            return View(books);
        }

        public static int StringToInt(String str)
        {
            if (int.TryParse(str, out int num))
            {
                return num;
            }
            return -1;
        }

        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            if (db.AddBook(book) == 0)
            {
                return RedirectToAction("Books");
            }
            else
            {
                ModelState.AddModelError("AddBook", "There is some error");
                return View();
            }
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult BookDetails(String addition)
        {
            Book book = db.GetBookById(addition);
            if (book != null)
            {
                return View(book);
            }
            else
            {
                ModelState.AddModelError("BookDetails", "There is some error");
                return RedirectToAction("Books");
            }
        }
    }
}