using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obooking.Models
{
    public class Book
    {
        [Key]
        [Display(Name = "ID")]
        public int BookId { get; set; }

        [Display(Name = "Book Name")]
        public string BookName { get; set; }

        [Display(Name = "Book Image")]
        public string ImageBook { get; set; }

        [Required]
        [Display(Name = "Book Description")]
        public string BooKDescription { get; set; }

        [Required]
        [Display(Name = "Book Popularity")]
        public string BookPopularity { get; set; }

        [Required]
        [Display(Name = "Book Price")]
        public int BookPrice { get; set; }

        [Required]
        [Display(Name = "Book Count")]
        public int BookCount { get; set; }

        [Required]
        [Display(Name = "Book Edition")]
        public string BookEdition { get; set; }

        [Required]
        [Display(Name = "Book Status")]
        public string BookStatus { get; set; }

        [Required]
        [Display(Name = "Category of Book")]
        public string BookCategory { get; set; }

        public int number_sold { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public Book() {
            number_sold = 0;
        }
        public Book(int i)
        {
            BookCategory = "anime";
            BookCount = 20;
            BooKDescription = "A once in alife time story";
            BookEdition = "first edition";
            BookName = "test";
            BookPopularity = "famous";
            BookPrice = 20;
            BookStatus = "available";
            //ImageBook = "default.jfif";
          //  ImageFile = "default.jfif";

        }

        public Book GetClone()
        {
            return (Book)this.MemberwiseClone();
        }

    }
}