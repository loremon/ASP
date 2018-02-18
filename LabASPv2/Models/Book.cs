using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LabASPv2.Models
{
    [Table(name: "Book")]
    public class Book
    {
        public Book()
        {
            Reviews = new List<Review>();
        }

        [Key]
        public int Id { get; set; }

        [Column, Required(ErrorMessage = "Название книги обязательно для заполнения")]
        public String Name { get; set; }

        [Column, Required(ErrorMessage = "Имя автора обязательно для заполнения")]
        public String Author { get; set; }

        [Column]
        public String Description { get; set; }

        [Column]
        public String Genre { get; set; }

        public void Update(Book newBook)
        {
            Name = newBook.Name;
            Author = newBook.Author;
            Description = newBook.Description;
            Genre = newBook.Genre;
        }

        public List<Review> Reviews { get; set; }
    }
}