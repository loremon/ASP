using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LabASPv2.Models
{
    public class DbInit : DropCreateDatabaseAlways<Db>
    {
        protected override void Seed(Db db)
        {
            Book book = new Book()
            {
                Name = "sd",
                Author = "sedf"
            };
            Review review = new Review()
            {
                AuthorName = "grdg",
                Text = "dsfe",
                BookId = 1
            };
            db.Books.Add(book);
            db.Reviews.Add(review);
            base.Seed(db);
        }
    }
}