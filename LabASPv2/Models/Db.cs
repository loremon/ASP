using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LabASPv2.Models
{
    public class Db : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public int AddBook(Book book)
        {
            try
            {
                Books.Add(book);
                this.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                return -1;
            }
            return 0;
        }

        public Book GetBookById(String addition)
        {
            if(addition == null || !int.TryParse(addition, out int id))
            {
                return null;
            }
            Book book = Books.Where(b => b.Id == id)
                .SingleOrDefault();
            // Yes, string to int again, but this method can be
            // called not only there.
            book.Reviews = GetGoodReviewsByBookId(addition);
            return book;
        }

        public List<Review> GetGoodReviewsByBookId(String addition)
        {
            if (addition == null || !int.TryParse(addition, out int bookId))
            {
                return new List<Review>();
            }
            return Reviews
                .Where(r => r.BookId == bookId && !r.Reported).ToList();
        }

        public int IncLikeCount(String addition)
        {
            if (addition == null || !int.TryParse(addition, out int reviewId))
            {
                return -1;
            }
            int newLikeCount = Reviews.Where(r => r.Id == reviewId).SingleOrDefault().Like();
            this.SaveChanges();
            return newLikeCount;
        }

        public List<Book> getBooks(int from, int to)
        {
            if (from < 0 || from >= Books.Count())
            {
                from = 0;
            }
            if (to < 0 || to >= Books.Count())
            {
                to = Books.Count() - 1;
            }
            return Books.ToList().Skip(from)
                .Take(to - from + 1).ToList();
        }

        public int AddReview(Review review)
        {
            try
            {
                Reviews.Add(review);
                this.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                return -1;
            }
            return 0;
        }

        public int ReportReview(String addition, String text)
        {
            try
            {
                if (addition == null || !int.TryParse(addition, out int reviewId))
                {
                    return -1;
                }
                Reviews.Where(r => r.Id == reviewId).SingleOrDefault().Report(text);
                this.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                return -1;
            }
            return 0;
        }
    }
}