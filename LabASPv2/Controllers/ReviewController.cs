using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabASPv2.Models;

namespace LabASPv2.Controllers
{
    public class ReviewController : Controller
    {
        public Db db = new Db();

        public ActionResult Index()
        {
            return RedirectToAction("Reviews");
        }

        public ActionResult Reviews()
        {
            return View(db.Reviews.ToList());
        }

        public ActionResult AddReview(String addition)
        {
            if (addition == null || !int.TryParse(addition, out int bookId))
            {
                return RedirectToAction("BookDetails", "Book", addition);
            }
            return View(new Review() { BookId = bookId });
        }

        [HttpPost]
        public ActionResult AddReview(Review review)
        {
            if (db.AddReview(review) == 0)
            {
                return RedirectToAction("BookDetails", "Book", new { addition = review.BookId.ToString() });
            }
            else
            {
                return View(review);
            }
        }

        [HttpPost]
        public string IncLikeCount(String addition)
        {
            return db.IncLikeCount(addition).ToString();
        }

        [HttpPost]
        public int ReportReview(String addition, String text)
        {
            return db.ReportReview(addition, text);
        }
    }
}