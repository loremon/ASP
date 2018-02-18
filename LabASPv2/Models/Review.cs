using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LabASPv2.Models
{
    [Table("Review")]
    public class Review
    {
        public Review()
        {
            LikeCount = 0;
            Reported = false;
        }

        [Key]
        public int Id { get; set; }

        [Column, Required(ErrorMessage = "Ваше имя обязательно для заполнения")]
        public String AuthorName { get; set; }

        [Column, Required(ErrorMessage = "Текст отзыва обязателен для заполнения")]
        public String Text { get; set; }

        [Column, Index]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        [Column]
        public int LikeCount { get; set; }

        [Column]
        public bool Reported { get; set; }

        [Column]
        public String ReportReason { get; set; }

        public int Like()
        {
            LikeCount++;
            return LikeCount;
        }

        public void Report(String reason)
        {
            Reported = true;
            ReportReason = reason;
        }
    }
}